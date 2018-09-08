namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblDriversStatus_And_tblVehiclesStatus_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DriverStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        AssignedRequestId = c.Int(nullable: false),
                        ConfigurationId = c.Int(),
                        OnTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedRequests", t => t.AssignedRequestId, cascadeDelete: false)
                .ForeignKey("dbo.Configurations", t => t.ConfigurationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.AssignedRequestId)
                .Index(t => t.ConfigurationId);
            
            CreateTable(
                "dbo.VehicleStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        VehicleId = c.Int(nullable: false),
                        AssignedRequestId = c.Int(nullable: false),
                        ConfigurationId = c.Int(),
                        OnTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedRequests", t => t.AssignedRequestId, cascadeDelete: false)
                .ForeignKey("dbo.Configurations", t => t.ConfigurationId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.AssignedRequestId)
                .Index(t => t.ConfigurationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleStatus", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleStatus", "ConfigurationId", "dbo.Configurations");
            DropForeignKey("dbo.VehicleStatus", "AssignedRequestId", "dbo.AssignedRequests");
            DropForeignKey("dbo.DriverStatus", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DriverStatus", "ConfigurationId", "dbo.Configurations");
            DropForeignKey("dbo.DriverStatus", "AssignedRequestId", "dbo.AssignedRequests");
            DropIndex("dbo.VehicleStatus", new[] { "ConfigurationId" });
            DropIndex("dbo.VehicleStatus", new[] { "AssignedRequestId" });
            DropIndex("dbo.VehicleStatus", new[] { "VehicleId" });
            DropIndex("dbo.DriverStatus", new[] { "ConfigurationId" });
            DropIndex("dbo.DriverStatus", new[] { "AssignedRequestId" });
            DropIndex("dbo.DriverStatus", new[] { "EmployeeId" });
            DropTable("dbo.VehicleStatus");
            DropTable("dbo.DriverStatus");
        }
    }
}
