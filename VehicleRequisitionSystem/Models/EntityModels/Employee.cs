using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }

        public bool IsDeleted { get; set; }

        public List<RequisitionRequest> RequisitionRequests { get; set; } 
    }
}