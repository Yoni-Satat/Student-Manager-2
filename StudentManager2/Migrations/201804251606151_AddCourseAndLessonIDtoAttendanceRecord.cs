namespace StudentManager2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseAndLessonIDtoAttendanceRecord : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AttendanceRecord", name: "Course_CourseID", newName: "CourseID");
            RenameColumn(table: "dbo.AttendanceRecord", name: "Lesson_LessonID", newName: "LessonID");
            RenameIndex(table: "dbo.AttendanceRecord", name: "IX_Course_CourseID", newName: "IX_CourseID");
            RenameIndex(table: "dbo.AttendanceRecord", name: "IX_Lesson_LessonID", newName: "IX_LessonID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AttendanceRecord", name: "IX_LessonID", newName: "IX_Lesson_LessonID");
            RenameIndex(table: "dbo.AttendanceRecord", name: "IX_CourseID", newName: "IX_Course_CourseID");
            RenameColumn(table: "dbo.AttendanceRecord", name: "LessonID", newName: "Lesson_LessonID");
            RenameColumn(table: "dbo.AttendanceRecord", name: "CourseID", newName: "Course_CourseID");
        }
    }
}
