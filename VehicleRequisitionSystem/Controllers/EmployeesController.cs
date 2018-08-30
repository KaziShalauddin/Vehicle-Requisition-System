using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

            var employees = db.Employees.Include(e => e.Department).Include(e=>e.Designation);
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
        public ActionResult EmployeeCreate()
        {
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            ViewBag.Designations = new SelectList(db.Designations.ToList(), "Id", "Name");
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            ViewBag.Designations = new SelectList(db.Designations.ToList(), "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        //[HttpPost]
        //public ActionResult Create(Employee employee)
        //{
        //    try
        //    {
        //        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        //        employee.UserId = user.Id;


        //        if (ModelState.IsValid)
        //        {
        //            db.Employees.Add(employee);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public JsonResult EmployeeJsonCreate(EmployeeEntryVM model)
        {
            //File Upload & Retrive
            // var file = model.ImageFile;

            //File SQL Upload & Retrive
            //VehicleRequisitionDBContext db = new VehicleRequisitionDBContext();
            //int imageId = 0;

            // ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


            var file = model.ImageFile;

            byte[] imagebyte = null;


            if (file != null)
            {
                //File Upload & Retrive
                //var fileName = Path.GetFileName(file.FileName);
                //var extention = Path.GetExtension(file.FileName);
                //var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);


                //File Upload & Retrive(both SQL and non-SQL)
                file.SaveAs(Server.MapPath("~/Images/" + file.FileName));

                //File SQL Upload & Retrive
                BinaryReader reader = new BinaryReader(file.InputStream);

                imagebyte = reader.ReadBytes(file.ContentLength);

                Employee img = new Employee();
                img.DepartmentId = model.DepartmentId;
                img.DesignationId = model.DesignationId;
                img.IsDriver = model.IsDriver;
                img.DrivingLicenseNo = model.DrivingLicenseNo;
                img.Address = model.Address;
                img.Email = model.Email;
                img.Phone = model.Phone;
                img.UserId = model.UserId;
                img.Name = model.Name;
                img.Image = imagebyte;
                img.ImagePath = "/Images/" + file.FileName;
                img.IsDeleted = false;
                db.Employees.Add(img);
                db.SaveChanges();
                


            }

            //File Upload & Retrive
            //return Json(file.FileName, JsonRequestBehavior.AllowGet);

            //File SQL Upload & Retrive
            return Json(JsonRequestBehavior.AllowGet);
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
