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

        //if gate pass done by gate man
        [Display(Name = "Gate Pass By me")]
        public bool GatePassBy { get; set; }

        //public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }

        [Display(Name = "Employee Id")]
        public string EmpIdNo { get; set; }

        [Required]
        [Display(Name = "Vehicle Registration No.")]
        public string RegistrationNo { get; set; }
        

        public int AssignedRequestId { get; set; }
        public AssignedRequest AssignedRequest { get; set; }

        public int? ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }

        public DateTime OnTime { get; set; }

       

       
    }
}
