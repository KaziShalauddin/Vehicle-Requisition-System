using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VehicleRequisitionSystem.Models;
using VehicleRequisitionSystem.Models.DBContext;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Models.ViewModels;

namespace VehicleRequisitionSystem.Controllers
{
    public class RequestsController : Controller
    {
        private VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();

        // GET: Requests
        public ActionResult Index()
        {
            List<RequestListVM> rsList = new List<RequestListVM>();
            var requestList = db.Requests.Include(e=>e.Configuration).ToList();
            foreach (var item in requestList)
            {
                var employee= db.Employees.Include(e=>e.Department).Include(e=>e.Designation).Where(e => e.EmpIdNo == item.EmpIdNo).ToList();
                var employeeName= employee.Select(e => e.Name).FirstOrDefault();
                var department = employee.Select(e => e.Department.Name).FirstOrDefault();
                var designation = employee.Select(e => e.Designation.Name).FirstOrDefault();
               
                RequestListVM rs = new RequestListVM();
                rs.Id = item.Id;
                rs.Description = item.Description;
                rs.Location = item.Location;
                rs.Persons = item.Persons;
                rs.DepartureTime = item.DepartureTime;
                rs.CheckInTime = item.CheckInTime;
                rs.EmpIdNo = item.EmpIdNo;
                rs.Name = employeeName;
                rs.Designation = designation;
                rs.Configuration = item.Configuration.Name;
                rs.Department = department;
                rs.IsCanceled = item.IsCanceled;
                rsList.Add(rs);
            }
            //var requests = db.Requests.Include(r => r.Configuration).Include(r => r.Employee).Include(r => r.Employee.Department).Include(r => r.Employee.Designation);
            return View(rsList);
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            //ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name");
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestEntryVM requestEntry)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Request request = new Request();
            if (ModelState.IsValid)
            {
                request.UserId = user.Id;
                request.Description = requestEntry.Description;
                request.Location = requestEntry.Location;
                request.Persons = requestEntry.Persons;
                request.DepartureTime = requestEntry.DepartureTime;
                request.CheckInTime = requestEntry.CheckInTime;
                request.EmpIdNo = requestEntry.EmployeeId;
                request.ConfigurationId = 3;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name", request.ConfigurationId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", request.EmployeeId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,Location,Persons,DepartureTime,CheckInTime,IsCanceled,IsDeleted,EmpIdNo,ConfigurationId,EmployeeId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConfigurationId = new SelectList(db.Configurations, "Id", "Name", request.ConfigurationId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", request.EmployeeId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
