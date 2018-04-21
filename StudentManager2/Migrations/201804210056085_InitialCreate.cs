namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(),
                        Level = c.String(),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Lesson",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        LocationID = c.Int(),
                        Topic = c.String(),
                        LessonStart = c.DateTime(),
                        LessonEnd = c.DateTime(),
                        Date = c.DateTime(),
                        IsMandatory = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Location", t => t.LocationID)
                .Index(t => t.CourseID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Building = c.String(),
                        RoomNumber = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.StudyGroup",
                c => new
                    {
                        StudyGroupID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        GroupTitle = c.String(),
                    })
                .PrimaryKey(t => t.StudyGroupID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MatricNumber = c.String(),
                        Gender = c.String(),
                        Adjustments = c.Boolean(nullable: false),
                        Origin = c.String(),
                        YearOfStudy = c.Int(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.StudentGroup",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.StudentID })
                .ForeignKey("dbo.StudyGroup", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGroup", "StudentID", "dbo.Student");
            DropForeignKey("dbo.StudentGroup", "GroupID", "dbo.StudyGroup");
            DropForeignKey("dbo.StudyGroup", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Lesson", "LocationID", "dbo.Location");
            DropForeignKey("dbo.Lesson", "CourseID", "dbo.Course");
            DropIndex("dbo.StudentGroup", new[] { "StudentID" });
            DropIndex("dbo.StudentGroup", new[] { "GroupID" });
            DropIndex("dbo.StudyGroup", new[] { "CourseID" });
            DropIndex("dbo.Lesson", new[] { "LocationID" });
            DropIndex("dbo.Lesson", new[] { "CourseID" });
            DropTable("dbo.StudentGroup");
            DropTable("dbo.Student");
            DropTable("dbo.StudyGroup");
            DropTable("dbo.Location");
            DropTable("dbo.Lesson");
            DropTable("dbo.Course");
        }
    }
}
