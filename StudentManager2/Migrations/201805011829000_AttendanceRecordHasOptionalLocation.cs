namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendanceRecordHasOptionalLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttendanceRecord", "LocationID", "dbo.Location");
            AddColumn("dbo.AttendanceRecord", "Location_LocationID", c => c.Int());
            CreateIndex("dbo.AttendanceRecord", "Location_LocationID");
            AddForeignKey("dbo.AttendanceRecord", "Location_LocationID", "dbo.Location", "LocationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceRecord", "Location_LocationID", "dbo.Location");
            DropIndex("dbo.AttendanceRecord", new[] { "Location_LocationID" });
            DropColumn("dbo.AttendanceRecord", "Location_LocationID");
            AddForeignKey("dbo.AttendanceRecord", "LocationID", "dbo.Location", "LocationID");
        }
    }
}
