﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRequisitionSystem.Models.EntityModels
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Department")]
        public string   Name { get; set; }

        public bool IsDeleted { get; set; }

        public List<Employee> employees { get; set; }

    }
}