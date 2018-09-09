using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
   public class CommentsVM
    {
        public int RequestId { get; set; }
        public string UserId { get; set; }
       
        [Display(Name = "Comments")]
        public string Description { get; set; }
        [Display(Name = "Comments By")]
        public string EmpIdNo { get; set; }
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        [Display(Name = "Designation")]
        public string DesignationName { get; set; }
        [Display(Name = "Comments Time")]
        public DateTime DateTime { get; set; }
    }
}
