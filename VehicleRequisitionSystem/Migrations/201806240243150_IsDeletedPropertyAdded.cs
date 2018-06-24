namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedPropertyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignedRequisitions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Drivers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequisitionRequests", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequisitionRequests", "AssignedRequisition_Id", c => c.Int());
            AddColumn("dbo.Vehicles", "IsDeleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.RequisitionRequests", "AssignedRequisition_Id");
            AddForeignKey("dbo.RequisitionRequests", "AssignedRequisition_Id", "dbo.AssignedRequisitions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionRequests", "AssignedRequisition_Id", "dbo.AssignedRequisitions");
            DropIndex("dbo.RequisitionRequests", new[] { "AssignedRequisition_Id" });
            DropColumn("dbo.Vehicles", "IsDeleted");
            DropColumn("dbo.RequisitionRequests", "AssignedRequisition_Id");
            DropColumn("dbo.RequisitionRequests", "IsDeleted");
            DropColumn("dbo.Employees", "IsDeleted");
            DropColumn("dbo.Drivers", "IsDeleted");
            DropColumn("dbo.AssignedRequisitions", "IsDeleted");
        }
    }
}
