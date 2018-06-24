using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class RequisitionRequest
    {
        public int Id { get; set; }
        public string Cause { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}