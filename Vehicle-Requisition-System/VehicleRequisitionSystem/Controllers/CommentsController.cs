using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VehicleRequisitionSystem.Models;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Models.ViewModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class CommentsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: Comments
        public ActionResult Index()
        {
            var commentses = db.Commentses.Include(c => c.Request);
            return View(commentses.ToList());
        }
        private static ApplicationUser ApplicationUser()
        {
            ApplicationUser user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user;
        }
        public ActionResult RequestSpecificIndex(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var commentses = db.Commentses.Include(c => c.Request).Where(e => e.RequestId == id);
            List<CommentsVM> commentsList = new List<CommentsVM>();

            foreach (var item in commentses)
            {
                var userId = item.UserId;
                CommentsVM commentsVm = new CommentsVM();
                commentsVm.UserId = userId;
                commentsVm.Description = item.Description;
                commentsVm.DateTime = item.DateTime;
                commentsVm.RequestId = item.RequestId;
                var commentsBy = db.Employees.Include(e => e.Department).Include(e => e.Designation).FirstOrDefault(e => e.UserId == userId);
               
                if (commentsBy != null)
                {
                    commentsVm.EmpIdNo = commentsBy.EmpIdNo;
                    commentsVm.Name = commentsBy.Name;
                    commentsVm.DepartmentName = commentsBy.Department.Name;
                    commentsVm.DesignationName = commentsBy.Designation.Name;

                }
                //db.Employees.Where(e => e.UserId == item.UserId).Select(e => e.EmpIdNo).FirstOrDefault();
               
                commentsList.Add(commentsVm);
            }
            ViewBag.UserId = ApplicationUser().Id;
            ViewBag.RequestId = id;
            return View(commentsList);
        }

        public JsonResult CreateCommentsJson(CommentsWithCreateVm model)
        {
           
            Comments cm=new Comments();
            cm.UserId = ApplicationUser().Id;
            cm.Description = model.Description;
            cm.DateTime = model.DateTime;
            cm.RequestId = model.RequestId;
            db.Commentses.Add(cm);
            db.SaveChanges();
            var commentses = db.Commentses.Include(c => c.Request).Where(e => e.RequestId == cm.RequestId);
           model.CommentsVms = new List<CommentsVM>();

            foreach (var item in commentses)
            {
                var userId = item.UserId;
                CommentsVM commentsVm = new CommentsVM();
                commentsVm.UserId = userId;
                commentsVm.Description = item.Description;
                commentsVm.DateTime = item.DateTime;
                commentsVm.RequestId = item.RequestId;
                var commentsBy = db.Employees.Include(e => e.Department).Include(e => e.Designation).FirstOrDefault(e => e.UserId == userId);

                if (commentsBy != null)
                {
                    commentsVm.EmpIdNo = commentsBy.EmpIdNo;
                    commentsVm.Name = commentsBy.Name;
                    commentsVm.DepartmentName = commentsBy.Department.Name;
                    commentsVm.DesignationName = commentsBy.Designation.Name;

                }
                //db.Employees.Where(e => e.UserId == item.UserId).Select(e => e.EmpIdNo).FirstOrDefault();

                model.CommentsVms.Add(commentsVm);
            }
            //ViewBag.UserId = ApplicationUser().Id;
            //ViewBag.RequestId = id;
            // return View(model);
            
         return Json(model.CommentsVms, JsonRequestBehavior.AllowGet);

    }

    
    // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Commentses.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }
        

        // GET: Comments/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.RequestId = id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comments comments)
        {
            var user = ApplicationUser();
            comments.UserId = user.Id;
            comments.DateTime = DateTime.Now;
            comments.RequestId = comments.Id;
            if (ModelState.IsValid)
            {
                db.Commentses.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comments);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Commentses.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            //ViewBag.RequestId = new SelectList(db.Requests, "Id", "UserId", comments.RequestId);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.RequestId = new SelectList(db.Requests, "Id", "UserId", comments.RequestId);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Commentses.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Commentses.Find(id);
            db.Commentses.Remove(comments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
