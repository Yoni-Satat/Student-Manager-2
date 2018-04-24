namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAttendanceRecordTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AttendanceRecord", "LocationID", c => c.Int());
            CreateIndex("dbo.AttendanceRecord", "LocationID");
            AddForeignKey("dbo.AttendanceRecord", "LocationID", "dbo.Location", "LocationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceRecord", "LocationID", "dbo.Location");
            DropIndex("dbo.AttendanceRecord", new[] { "LocationID" });
            AlterColumn("dbo.AttendanceRecord", "LocationID", c => c.Int(nullable: false));
        }
    }
}
