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
    [Authorize]
    public class RequisitionRequestsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: RequisitionRequests
        public ActionResult Index()
        {
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            //var requisitions = db.Requisitions.Include(r => r.Employee);
            return View(db.Requisitions.ToList());
        }
        public ActionResult EmployeeRequisitionsList()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
           
            var requisitions = db.Requisitions.Where(r => r.UserId==user.Id);
            return View(requisitions.ToList());
        }
        // GET: RequisitionRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            if (requisitionRequest == null)
            {
                return HttpNotFound();
            }
            return View(requisitionRequest);
        }

        // GET: RequisitionRequests/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: RequisitionRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cause,CheckInTime,DepartureTime,Status,IsDeleted,EmployeeId")] RequisitionRequest requisitionRequest)
        {
            if (ModelState.IsValid)
            {
                db.Requisitions.Add(requisitionRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", requisitionRequest.EmployeeId);
            return View(requisitionRequest);
        }

        // GET: RequisitionRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            if (requisitionRequest == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", requisitionRequest.EmployeeId);
            return View(requisitionRequest);
        }

        // POST: RequisitionRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cause,CheckInTime,DepartureTime,Status,IsDeleted,EmployeeId")] RequisitionRequest requisitionRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisitionRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", requisitionRequest.EmployeeId);
            return View(requisitionRequest);
        }

        // GET: RequisitionRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            if (requisitionRequest == null)
            {
                return HttpNotFound();
            }
            return View(requisitionRequest);
        }

        // POST: RequisitionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitionRequest requisitionRequest = db.Requisitions.Find(id);
            db.Requisitions.Remove(requisitionRequest);
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

        // GET: RequisitionRequests/Create
        public ActionResult CreateRequisitionRequest()
        {
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: RequisitionRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequisitionRequest(EmployeeRequisitionRequestVM requisitionRequest)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                RequisitionRequest request=new RequisitionRequest();
                request.UserId = user.Id;
                request.Cause = requisitionRequest.Cause;
                request.DepartureTime = requisitionRequest.DepartureTime;
                request.CheckInTime = requisitionRequest.CheckInTime;
                request.Status = requisitionRequest.Status;
                //request.EmployeeId = db.Employees.Where(r => r.UserId == user.Id).Select(r =>r.Id).FirstOrDefault();
                db.Requisitions.Add(request);
                db.SaveChanges();
                return RedirectToAction("EmployeeRequisitionsList");
            }

           
            return View(requisitionRequest);
        }
    }
}
