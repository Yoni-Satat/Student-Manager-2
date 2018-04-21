namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLessonNPtoStudyGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lesson", "StudyGroup_StudyGroupID", c => c.Int());
            CreateIndex("dbo.Lesson", "StudyGroup_StudyGroupID");
            AddForeignKey("dbo.Lesson", "StudyGroup_StudyGroupID", "dbo.StudyGroup", "StudyGroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lesson", "StudyGroup_StudyGroupID", "dbo.StudyGroup");
            DropIndex("dbo.Lesson", new[] { "StudyGroup_StudyGroupID" });
            DropColumn("dbo.Lesson", "StudyGroup_StudyGroupID");
        }
    }
}
