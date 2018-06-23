using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Status { get; set; }

        public List<AssignedRequisition> AssignedRequisitions { get; set; }
    }
}