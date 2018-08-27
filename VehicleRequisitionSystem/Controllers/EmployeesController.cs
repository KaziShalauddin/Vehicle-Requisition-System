using System;
using System.Collections;
using System.Collections.Generic;
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
    public class EmployeesController : Controller
    {
        public VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
        // GET: Employees
        public ActionResult Index()
        {

            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);

        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                employee.UserId = user.Id;


                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
    }
}
