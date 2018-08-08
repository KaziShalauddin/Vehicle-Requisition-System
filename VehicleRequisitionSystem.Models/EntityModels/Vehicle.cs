using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string RegistrationNo { get; set; }
        public string ChesisNo { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public List<AssignedRequisition> AssignedRequisitions { get; set; }
    }
}