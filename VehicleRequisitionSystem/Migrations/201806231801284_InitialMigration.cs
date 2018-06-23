namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedRequisitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        RequisitionRequestId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.RequisitionRequests", t => t.RequisitionRequestId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.RequisitionRequestId)
                .Index(t => t.DriverId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CheckInTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Designation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequisitionRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cause = c.String(),
                        CheckInTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedRequisitions", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.AssignedRequisitions", "RequisitionRequestId", "dbo.RequisitionRequests");
            DropForeignKey("dbo.AssignedRequisitions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.RequisitionRequests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AssignedRequisitions", "DriverId", "dbo.Drivers");
            DropIndex("dbo.RequisitionRequests", new[] { "EmployeeId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "VehicleId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "DriverId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "RequisitionRequestId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "EmployeeId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.RequisitionRequests");
            DropTable("dbo.Employees");
            DropTable("dbo.Drivers");
            DropTable("dbo.AssignedRequisitions");
        }
    }
}
