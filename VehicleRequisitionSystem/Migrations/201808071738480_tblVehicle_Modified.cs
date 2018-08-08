namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblVehicle_Modified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "BrandName", c => c.String());
            AddColumn("dbo.Vehicles", "Model", c => c.String());
            AddColumn("dbo.Vehicles", "RegistrationNo", c => c.String());
            AddColumn("dbo.Vehicles", "ChesisNo", c => c.String());
            DropColumn("dbo.Vehicles", "No");
            DropColumn("dbo.Vehicles", "DepartureTime");
            DropColumn("dbo.Vehicles", "CheckInTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "CheckInTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "DepartureTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "No", c => c.String());
            DropColumn("dbo.Vehicles", "ChesisNo");
            DropColumn("dbo.Vehicles", "RegistrationNo");
            DropColumn("dbo.Vehicles", "Model");
            DropColumn("dbo.Vehicles", "BrandName");
        }
    }
}
