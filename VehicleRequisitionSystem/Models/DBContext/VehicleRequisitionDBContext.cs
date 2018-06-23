using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.DBContext
{
    public class VehicleRequisitionDBContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RequisitionRequest> Requisitions { get; set; }
        public DbSet<AssignedRequisition> AssignedRequisitions { get; set; }
        
    }
}