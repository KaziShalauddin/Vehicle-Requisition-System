namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblDepartment_Modified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Name", c => c.String());
            DropColumn("dbo.Departments", "DepartmentName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "DepartmentName", c => c.String());
            DropColumn("dbo.Departments", "Name");
        }
    }
}
