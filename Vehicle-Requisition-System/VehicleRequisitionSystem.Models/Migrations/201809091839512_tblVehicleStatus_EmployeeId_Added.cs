namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblVehicleStatus_EmployeeId_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleStatus", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.VehicleStatus", "EmployeeId");
            AddForeignKey("dbo.VehicleStatus", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleStatus", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.VehicleStatus", new[] { "EmployeeId" });
            DropColumn("dbo.VehicleStatus", "EmployeeId");
        }
    }
}
