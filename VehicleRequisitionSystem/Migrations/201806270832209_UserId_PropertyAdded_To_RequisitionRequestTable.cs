namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId_PropertyAdded_To_RequisitionRequestTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionRequests", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionRequests", "UserId");
        }
    }
}
