namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOptionalRelatiomship2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lesson", "LocationID", "dbo.Location");
            AddColumn("dbo.Lesson", "Location_LocationID", c => c.Int());
            CreateIndex("dbo.Lesson", "Location_LocationID");
            AddForeignKey("dbo.Lesson", "Location_LocationID", "dbo.Location", "LocationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lesson", "Location_LocationID", "dbo.Location");
            DropIndex("dbo.Lesson", new[] { "Location_LocationID" });
            DropColumn("dbo.Lesson", "Location_LocationID");
            AddForeignKey("dbo.Lesson", "LocationID", "dbo.Location", "LocationID");
        }
    }
}
