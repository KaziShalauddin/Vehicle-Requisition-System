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
            List<GatePassListVM> gatePass = new List<GatePassListVM>();

            var driversStatuses = db.DriversStatuses.OrderByDescending(e => e.Id).Include(d => d.AssignedRequest).Include(d => d.Configuration).Include(d => d.Employee);
            var vehicleStatuses = db.VehiclesStatuses.OrderByDescending(e => e.Id).Include(d => d.AssignedRequest).Include(d => d.Configuration).Include(d => d.Employee);
            var gatePassList = (from driver in driversStatuses
                                let vehicle = vehicleStatuses.FirstOrDefault(vehicle => vehicle.EmployeeId == driver.EmployeeId)

                                select new
                                {

                                    driver.Employee.EmpIdNo,
                                    DriverName = driver.Employee.Name,
                                    vehicle.Vehicle.BrandName,
                                    vehicle.Vehicle.RegistrationNo,
                                    Status = driver.Configuration.Name,
                                    driver.OnTime
                                });

            var itemCount = gatePassList.Count();
            foreach (var item in gatePassList)
            {
                GatePassListVM gatePassVm = new GatePassListVM();
                gatePassVm.EmpIdNo = item.EmpIdNo;
                gatePassVm.DriverName = item.DriverName;
                gatePassVm.Brand = item.BrandName;
                gatePassVm.RegistrationNo = item.RegistrationNo;
                gatePassVm.Status = item.Status;
                gatePassVm.OnTime = item.OnTime;
                gatePass.Add(gatePassVm);
            }
            return View(gatePass);
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
            var status =
                db.Configurations.Include(e => e.ConfigurationType)
                    .Where(e => e.ConfigurationType.Type == "DriverStatus");
            ViewBag.ConfigurationId = new SelectList(status, "Id", "Name");
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
                var vehicleId = db.Vehicles.Where(e => e.RegistrationNo == gatePass.RegistrationNo).Select(e => e.Id).FirstOrDefault();


                DriverStatus dv = new DriverStatus();
                dv.UserId = user.Id;
                dv.AssignedRequestId = gatePass.AssignedRequestId;
                dv.ConfigurationId = gatePass.ConfigurationId;
                dv.OnTime = DateTime.Now;

                VehicleStatus vs = new VehicleStatus();
                vs.VehicleId = vehicleId;
                vs.AssignedRequestId = gatePass.AssignedRequestId;
                vs.ConfigurationId = gatePass.ConfigurationId;
                vs.OnTime = DateTime.Now;
                vs.UserId = user.Id;

                if (gatePass.GatePassBy)
                {
                    var driverId = db.Employees.Where(e => e.UserId == dv.UserId).Select(e => e.Id).FirstOrDefault();
                    dv.EmployeeId = driverId;
                    vs.EmployeeId = dv.EmployeeId;
                }
                else
                {
                    var driverId = db.Employees.Where(e => e.EmpIdNo == gatePass.EmpIdNo && e.IsDriver == true).Select(e => e.Id).FirstOrDefault();
                    dv.EmployeeId = driverId;
                    vs.EmployeeId = dv.EmployeeId;
                }
                db.DriversStatuses.Add(dv);
                // db.SaveChanges();
                db.VehiclesStatuses.Add(vs);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.AssignedRequestId = new SelectList(db.AssignedRequests, "Id", "UserId", driverStatus.AssignedRequestId);
            // ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", driverStatus.EmployeeId);
            var status =
                db.Configurations.Include(e => e.ConfigurationType)
                    .Where(e => e.ConfigurationType.Type == "DriverStatus");
            ViewBag.ConfigurationId = new SelectList(status, "Id", "Name");

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
