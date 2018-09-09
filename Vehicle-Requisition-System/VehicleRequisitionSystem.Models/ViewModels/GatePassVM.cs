using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
   public class GatePassVM
    {
        [Display(Name = "Status Changed By")]
        public string UserId { get; set; }

        //public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }
        [Display(Name = "Employee Id")]
        public string EmpIdNo { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int AssignedRequestId { get; set; }
        public AssignedRequest AssignedRequest { get; set; }

        public int? ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }

        public DateTime OnTime { get; set; }

       

       
    }
}
