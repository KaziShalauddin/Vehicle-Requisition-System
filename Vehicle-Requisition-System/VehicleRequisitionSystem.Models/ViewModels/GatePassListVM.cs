using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
   public class GatePassListVM
    {
        //[Display(Name = "Status Changed By")]
        //public string UserId { get; set; }
        
        [Display(Name = "Employee Id")]
        public string EmpIdNo { get; set; }

        [Display(Name = "Driver")]
        public string DriverName { get; set; }

        [Display(Name = "Vehicle Brand")]
        public string Brand { get; set; }

        [Display(Name = "Vehicle Registration No.")]
        public string RegistrationNo { get; set; }
        
        public string Status { get; set; }

        public DateTime OnTime { get; set; }

       

       
    }
}
