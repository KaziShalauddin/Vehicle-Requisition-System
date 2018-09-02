using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class RequestEntryVM
    {
        public int Id { get; set; }


        public string UserId { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }
        public int Persons { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckInTime { get; set; }
        
        
        [Display(Name = "Request For Other")]
        public bool RequestFor { get; set; }


        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
