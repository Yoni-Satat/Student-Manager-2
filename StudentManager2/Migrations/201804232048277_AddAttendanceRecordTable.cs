namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceRecordTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceRecord",
                c => new
                    {
                        AttendanceRecordID = c.Int(nullable: false, identity: true),
                        StudyGroupID = c.Int(),
                        TutorName = c.String(),
                        Notes = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        LocationID = c.Int(nullable: false),
                        Lesson_LessonID = c.Int(),
                        Course_CourseID = c.Int(),
                    })
                .PrimaryKey(t => t.AttendanceRecordID)
                .ForeignKey("dbo.Lesson", t => t.Lesson_LessonID)
                .ForeignKey("dbo.Course", t => t.Course_CourseID)
                .ForeignKey("dbo.StudyGroup", t => t.StudyGroupID)
                .Index(t => t.StudyGroupID)
                .Index(t => t.Lesson_LessonID)
                .Index(t => t.Course_CourseID);
            
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
            DropForeignKey("dbo.AttendanceRecord", "Course_CourseID", "dbo.Course");
            DropForeignKey("dbo.StudentAttendanceRecord", "AttendanceRecordID", "dbo.AttendanceRecord");
            DropForeignKey("dbo.StudentAttendanceRecord", "StudentID", "dbo.Student");
            DropForeignKey("dbo.AttendanceRecord", "Lesson_LessonID", "dbo.Lesson");
            DropIndex("dbo.StudentAttendanceRecord", new[] { "AttendanceRecordID" });
            DropIndex("dbo.StudentAttendanceRecord", new[] { "StudentID" });
            DropIndex("dbo.AttendanceRecord", new[] { "Course_CourseID" });
            DropIndex("dbo.AttendanceRecord", new[] { "Lesson_LessonID" });
            DropIndex("dbo.AttendanceRecord", new[] { "StudyGroupID" });
            DropTable("dbo.StudentAttendanceRecord");
            DropTable("dbo.AttendanceRecord");
        }
    }
}
