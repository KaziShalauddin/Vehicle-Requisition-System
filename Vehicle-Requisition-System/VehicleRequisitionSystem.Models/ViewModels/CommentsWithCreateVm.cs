using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.ViewModels
{
    public class CommentsWithCreateVm
    {
        public int RequestId { get; set; }
        public Request Request { get; set; }
        [Required]
        [Display(Name = "Comments")]
        public string Description { get; set; }
        [Display(Name = "Comments By")]
        public string UserId { get; set; }
        [Display(Name = "Comments Time")]
        public DateTime DateTime { get; set; }

        public List<CommentsVM> CommentsVms { get; set; }
    }
}
