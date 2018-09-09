using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Comments
    {
        public int Id { get; set; }
      
        public int RequestId { get; set; }
        public Request Request { get; set; }
        [Required]
        [Display(Name = "Comments")]
        public string Description { get; set; }
        [Display(Name = "Comments By")]
        public string UserId { get; set; }
        [Display(Name = "Comments Time")]
        public DateTime DateTime { get; set; }
    }
}
