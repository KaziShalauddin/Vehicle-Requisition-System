namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblDepartment_Added_And_tblEmployee_tblRequisition_Modified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequisitionRequests", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.RequisitionRequests", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.RequisitionRequests", new[] { "VehicleId" });
            DropIndex("dbo.RequisitionRequests", new[] { "DriverId" });
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "Phone", c => c.String());
            AddColumn("dbo.Employees", "Email", c => c.String());
            AddColumn("dbo.Employees", "Address", c => c.String());
            AddColumn("dbo.Employees", "DepartmentId", c => c.Int());
            AddColumn("dbo.RequisitionRequests", "Description", c => c.String());
            AddColumn("dbo.RequisitionRequests", "Location", c => c.String());
            AddColumn("dbo.RequisitionRequests", "Persons", c => c.Int(nullable: false));
            AddColumn("dbo.RequisitionRequests", "StatusId", c => c.Int());
            AddColumn("dbo.RequisitionRequests", "AssignedRequisitionId", c => c.Int());
            CreateIndex("dbo.Employees", "DepartmentId");
            CreateIndex("dbo.RequisitionRequests", "StatusId");
            CreateIndex("dbo.RequisitionRequests", "AssignedRequisitionId");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.RequisitionRequests", "AssignedRequisitionId", "dbo.AssignedRequisitions", "Id");
            AddForeignKey("dbo.RequisitionRequests", "StatusId", "dbo.Status", "Id");
            DropColumn("dbo.RequisitionRequests", "Cause");
            DropColumn("dbo.RequisitionRequests", "Place");
            DropColumn("dbo.RequisitionRequests", "IsAssigned");
            DropColumn("dbo.RequisitionRequests", "VehicleId");
            DropColumn("dbo.RequisitionRequests", "DriverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionRequests", "DriverId", c => c.Int());
            AddColumn("dbo.RequisitionRequests", "VehicleId", c => c.Int());
            AddColumn("dbo.RequisitionRequests", "IsAssigned", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequisitionRequests", "Place", c => c.String());
            AddColumn("dbo.RequisitionRequests", "Cause", c => c.String());
            DropForeignKey("dbo.RequisitionRequests", "StatusId", "dbo.Status");
            DropForeignKey("dbo.RequisitionRequests", "AssignedRequisitionId", "dbo.AssignedRequisitions");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.RequisitionRequests", new[] { "AssignedRequisitionId" });
            DropIndex("dbo.RequisitionRequests", new[] { "StatusId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropColumn("dbo.RequisitionRequests", "AssignedRequisitionId");
            DropColumn("dbo.RequisitionRequests", "StatusId");
            DropColumn("dbo.RequisitionRequests", "Persons");
            DropColumn("dbo.RequisitionRequests", "Location");
            DropColumn("dbo.RequisitionRequests", "Description");
            DropColumn("dbo.Employees", "DepartmentId");
            DropColumn("dbo.Employees", "Address");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "Phone");
            DropTable("dbo.Departments");
            CreateIndex("dbo.RequisitionRequests", "DriverId");
            CreateIndex("dbo.RequisitionRequests", "VehicleId");
            AddForeignKey("dbo.RequisitionRequests", "VehicleId", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.RequisitionRequests", "DriverId", "dbo.Drivers", "Id");
        }
    }
}
