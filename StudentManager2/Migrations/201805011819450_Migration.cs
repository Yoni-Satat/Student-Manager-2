namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceRecord",
                c => new
                    {
                        AttendanceRecordID = c.Int(nullable: false, identity: true),
                        StudyGroupID = c.Int(),
                        LocationID = c.Int(),
                        CourseID = c.Int(),
                        LessonID = c.Int(),
                        TutorName = c.String(),
                        Notes = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AttendanceRecordID)
                .ForeignKey("dbo.Lesson", t => t.LessonID)
                .ForeignKey("dbo.Location", t => t.LocationID)
                .ForeignKey("dbo.Course", t => t.CourseID)
                .ForeignKey("dbo.StudyGroup", t => t.StudyGroupID)
                .Index(t => t.StudyGroupID)
                .Index(t => t.LocationID)
                .Index(t => t.CourseID)
                .Index(t => t.LessonID);
            
            CreateTable(
                "dbo.StudentAttendanceRecord",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        AttendanceRecordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentID, t.AttendanceRecordID })
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.AttendanceRecord", t => t.AttendanceRecordID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.AttendanceRecordID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceRecord", "StudyGroupID", "dbo.StudyGroup");
            DropForeignKey("dbo.AttendanceRecord", "CourseID", "dbo.Course");
            DropForeignKey("dbo.StudentAttendanceRecord", "AttendanceRecordID", "dbo.AttendanceRecord");
            DropForeignKey("dbo.StudentAttendanceRecord", "StudentID", "dbo.Student");
            DropForeignKey("dbo.AttendanceRecord", "LocationID", "dbo.Location");
            DropForeignKey("dbo.AttendanceRecord", "LessonID", "dbo.Lesson");
            DropIndex("dbo.StudentAttendanceRecord", new[] { "AttendanceRecordID" });
            DropIndex("dbo.StudentAttendanceRecord", new[] { "StudentID" });
            DropIndex("dbo.AttendanceRecord", new[] { "LessonID" });
            DropIndex("dbo.AttendanceRecord", new[] { "CourseID" });
            DropIndex("dbo.AttendanceRecord", new[] { "LocationID" });
            DropIndex("dbo.AttendanceRecord", new[] { "StudyGroupID" });
            DropTable("dbo.StudentAttendanceRecord");
            DropTable("dbo.AttendanceRecord");
        }
    }
}
