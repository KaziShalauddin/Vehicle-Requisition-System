namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblAssignedRequisition_And_tblRequisitionRequest_Deleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignedRequisitions", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.AssignedRequisitions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AssignedRequisitions", "RequisitionRequestId", "dbo.RequisitionRequests");
            DropForeignKey("dbo.RequisitionRequests", "AssignedRequisition_Id", "dbo.AssignedRequisitions");
            DropForeignKey("dbo.AssignedRequisitions", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RequisitionRequests", "AssignedRequisitionId", "dbo.AssignedRequisitions");
            DropForeignKey("dbo.RequisitionRequests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.RequisitionRequests", "StatusId", "dbo.Status");
            DropIndex("dbo.RequisitionRequests", new[] { "EmployeeId" });
            DropIndex("dbo.RequisitionRequests", new[] { "StatusId" });
            DropIndex("dbo.RequisitionRequests", new[] { "AssignedRequisitionId" });
            DropIndex("dbo.RequisitionRequests", new[] { "AssignedRequisition_Id" });
            DropIndex("dbo.AssignedRequisitions", new[] { "EmployeeId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "RequisitionRequestId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "DriverId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "VehicleId" });
            AddColumn("dbo.AssignedRequests", "Driver_Id", c => c.Int());
            AddColumn("dbo.Requests", "Employee_Id", c => c.Int());
            CreateIndex("dbo.AssignedRequests", "Driver_Id");
            CreateIndex("dbo.Requests", "Employee_Id");
            AddForeignKey("dbo.Requests", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.AssignedRequests", "Driver_Id", "dbo.Drivers", "Id");
            DropTable("dbo.RequisitionRequests");
            DropTable("dbo.AssignedRequisitions");
        }
        
        public override void Down()
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
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequisitionRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                        Persons = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        IsCanceled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        EmployeeId = c.Int(),
                        StatusId = c.Int(),
                        AssignedRequisitionId = c.Int(),
                        AssignedRequisition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.AssignedRequests", "Driver_Id", "dbo.Drivers");
            DropForeignKey("dbo.Requests", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Requests", new[] { "Employee_Id" });
            DropIndex("dbo.AssignedRequests", new[] { "Driver_Id" });
            DropColumn("dbo.Requests", "Employee_Id");
            DropColumn("dbo.AssignedRequests", "Driver_Id");
            CreateIndex("dbo.AssignedRequisitions", "VehicleId");
            CreateIndex("dbo.AssignedRequisitions", "DriverId");
            CreateIndex("dbo.AssignedRequisitions", "RequisitionRequestId");
            CreateIndex("dbo.AssignedRequisitions", "EmployeeId");
            CreateIndex("dbo.RequisitionRequests", "AssignedRequisition_Id");
            CreateIndex("dbo.RequisitionRequests", "AssignedRequisitionId");
            CreateIndex("dbo.RequisitionRequests", "StatusId");
            CreateIndex("dbo.RequisitionRequests", "EmployeeId");
            AddForeignKey("dbo.RequisitionRequests", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.RequisitionRequests", "EmployeeId", "dbo.Employees", "Id");
            AddForeignKey("dbo.RequisitionRequests", "AssignedRequisitionId", "dbo.AssignedRequisitions", "Id");
            AddForeignKey("dbo.AssignedRequisitions", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequisitionRequests", "AssignedRequisition_Id", "dbo.AssignedRequisitions", "Id");
            AddForeignKey("dbo.AssignedRequisitions", "RequisitionRequestId", "dbo.RequisitionRequests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignedRequisitions", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignedRequisitions", "DriverId", "dbo.Drivers", "Id", cascadeDelete: true);
        }
    }
}
