using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class DriverAssignedDetailsVM
    {
        public int AssignedId { get; set; }

        //[Display(Name = "Driver")]
        //public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }

        [Display(Name = "Vehicle Brand Name")]
        public string BrandName { get; set; }
        [Display(Name = "Vehicle  Registration No. :")]
        public string RegistrationNo { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Assigned For")]
        public string EmpIdNo { get; set; }

        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        [Display(Name = "Designation")]
        public string DesignationName { get; set; }
        public string Phone { get; set; }

    }
}
