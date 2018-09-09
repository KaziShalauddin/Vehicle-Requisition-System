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
    public class GatePassController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: DriverStatus
        public ActionResult Index()
        {
            var driversStatuses = db.DriversStatuses.Include(d => d.AssignedRequest).Include(d => d.Configuration).Include(d => d.Employee);
            return View(driversStatuses.ToList());
        }

        // GET: DriverStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverStatus driverStatus = db.DriversStatuses.Find(id);
            if (driverStatus == null)
            {
                return HttpNotFound();
            }
            return View(driverStatus);
        }

        // GET: DriverStatus/Create
        public ActionResult Generate()
        {
            //ViewBag.AssignedRequestId = new SelectList(db.AssignedRequests, "Id", "UserId");
            ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name");
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: DriverStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generate(GatePassVM gatePass)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


            if (ModelState.IsValid)
            {
                DriverStatus dv=new DriverStatus();
                //dv.EmployeeId = gatePass.EmpIdNo;
                VehicleStatus vs= new VehicleStatus();
                
                //db.DriversStatuses.Add(driverStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AssignedRequestId = new SelectList(db.AssignedRequests, "Id", "UserId", driverStatus.AssignedRequestId);
            ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name");
           // ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", driverStatus.EmployeeId);
            return View();
        }

        // GET: DriverStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverStatus driverStatus = db.DriversStatuses.Find(id);
            if (driverStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedRequestId = new SelectList(db.AssignedRequests, "Id", "UserId", driverStatus.AssignedRequestId);
            ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name", driverStatus.ConfigurationId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", driverStatus.EmployeeId);
            return View(driverStatus);
        }

        // POST: DriverStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,EmployeeId,AssignedRequestId,ConfigurationId,OnTime,IsDeleted")] DriverStatus driverStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedRequestId = new SelectList(db.AssignedRequests, "Id", "UserId", driverStatus.AssignedRequestId);
            ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name", driverStatus.ConfigurationId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", driverStatus.EmployeeId);
            return View(driverStatus);
        }

        // GET: DriverStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverStatus driverStatus = db.DriversStatuses.Find(id);
            if (driverStatus == null)
            {
                return HttpNotFound();
            }
            return View(driverStatus);
        }

        // POST: DriverStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DriverStatus driverStatus = db.DriversStatuses.Find(id);
            db.DriversStatuses.Remove(driverStatus);
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
