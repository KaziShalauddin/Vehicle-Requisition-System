using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class EmployeeRequisitionRequestVM
    {
       
        public string Cause { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Status { get; set; }

       
    }
}