namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblDrivers_And_tblStatus_Deleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignedRequests", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.AssignedRequests", new[] { "Driver_Id" });
            DropColumn("dbo.AssignedRequests", "Driver_Id");
            DropTable("dbo.Drivers");
            DropTable("dbo.Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DepartureTime = c.DateTime(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AssignedRequests", "Driver_Id", c => c.Int());
            CreateIndex("dbo.AssignedRequests", "Driver_Id");
            AddForeignKey("dbo.AssignedRequests", "Driver_Id", "dbo.Drivers", "Id");
        }
    }
}
