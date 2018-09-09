using System.Data.Entity;
using VehicleRequisitionSystem.Models.EntityModels;

namespace VehicleRequisitionSystem.Models.DBContext
{
    public class VehicleRequisitionDBContext : DbContext
    {

        public VehicleRequisitionDBContext() : base("name=DefaultConnection")
        {
            //
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DriverStatus> DriversStatuses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleStatus> VehiclesStatuses { get; set; }
       
        public DbSet<Request> Requests { get; set; }
        public DbSet<Comments> Commentses { get; set; }
        
        public DbSet<AssignedRequest> AssignedRequests { get; set; }

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<ConfigurationType> ConfigurationTypes { get; set; }
       // public DbSet<Status> Statuses { get; set; }

       
    }
}