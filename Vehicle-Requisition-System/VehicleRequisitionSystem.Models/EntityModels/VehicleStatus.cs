using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class VehicleStatus
    {
        public int Id { get; set; }

        [Display(Name="Status Changed By")] 
        public string UserId { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int AssignedRequestId { get; set; }
        public AssignedRequest AssignedRequest { get; set; }

        public int? ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }

        public DateTime OnTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
