namespace StudentManager2.Migrations
{
    using StudentManager2.DAL;
    using StudentManager2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
        }

        protected override void Seed(StudentContext context)
        {
            var Location = new List<Location>
            {
                new Location{Building="CMB",RoomNumber=1.12,Notes="Take Laptop with HDMI"},
                new Location{Building="TL",RoomNumber=2.56,Notes="Ask Euan for projector"},
                new Location{Building="Main Library",RoomNumber=3.12,Notes="Quite please..."},
                new Location{Building="King's Building",RoomNumber=1.22,Notes="Mind the gap"},
                new Location{Building="TLV",RoomNumber=1.23,Notes="Good for lab work"},
                new Location{Building="BLT",RoomNumber=3.12,Notes="Close to the cfetiria"}
            };
            Location.ForEach(s => context.Locations.AddOrUpdate(p => p.LocationID, s));
            context.SaveChanges();

            var students = new List<Student>
            {
            new Student{FirstName="Carson",LastName="Alexander",DateOfBirth=DateTime.Parse("2005-09-01"),
                        MatricNumber ="SN0001", Gender="Male",
                        Adjustments=true, Origin="UK", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Meredith",LastName="Alonso",DateOfBirth=DateTime.Parse("2002-04-11"),
                        MatricNumber ="SN0002", Gender="Female",
                        Adjustments=true, Origin="UK", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Arturo",LastName="Anand",DateOfBirth=DateTime.Parse("2003-09-01"),
                        MatricNumber ="SN0003", Gender="Male",
                        Adjustments=true, Origin="EU", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Gytis",LastName="Barzdukas",DateOfBirth=DateTime.Parse("2002-09-01"),
                        MatricNumber ="SN0004", Gender="Male",
                        Adjustments=false, Origin="USA", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Yan",LastName="Li",DateOfBirth=DateTime.Parse("2002-09-01"),
                        MatricNumber ="SN0005", Gender="Male",
                        Adjustments=true, Origin="UK", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Peggy",LastName="Justice",DateOfBirth=DateTime.Parse("2001-09-01"),
                        MatricNumber ="SN0006", Gender="Female",
                        Adjustments=false, Origin="UK", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Laura",LastName="Norman",DateOfBirth=DateTime.Parse("2003-09-01"),
                        MatricNumber ="SN0007", Gender="Female",
                        Adjustments=false, Origin="UK", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()},
            new Student{FirstName="Nino",LastName="Olivetto",DateOfBirth=DateTime.Parse("2005-09-01"),
                        MatricNumber ="SN0008", Gender="Male",
                        Adjustments=false, Origin="UK", YearOfStudy=2018, ImageURL="", StudyGroups = new List<StudyGroup>()}
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.StudentID, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
            new Course{CourseID=1000,Title="Statistic 101",Level="A"},
            new Course{CourseID=1001,Title="Advanced Statistic",Level="A"},
            new Course{CourseID=1002,Title="Statistic in Education",Level="A"},
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var lessons = new List<Lesson>
            {
                new Lesson{CourseID=1000,LocationID=1,Topic="Welcome and Induction"},
                new Lesson{CourseID=1000,LocationID=2,Topic="Regression to the mean"},
                new Lesson{CourseID=1000,LocationID=3,Topic="Variables"},
                new Lesson{CourseID=1000,LocationID=2,Topic="Constant"},
                new Lesson{CourseID=1000,LocationID=1,Topic="All day lab"},
                new Lesson{CourseID=1000,LocationID=3,Topic="Presentations"},
                new Lesson{CourseID=1001,LocationID=1,Topic="First Lesson"},
                new Lesson{CourseID=1001,LocationID=2,Topic="Second Lesson"},
                new Lesson{CourseID=1001,LocationID=3,Topic="Third Lesson"},
                new Lesson{CourseID=1001,LocationID=2,Topic="Fourth Lesson"},
                new Lesson{CourseID=1001,LocationID=1,Topic="Fifth Lesson"},
                new Lesson{CourseID=1001,LocationID=3,Topic="Sixth Lesson"}
            };
            lessons.ForEach(s => context.Lessons.AddOrUpdate(p => p.LessonID, s));
            context.SaveChanges();

            var StudyStudyGroups = new List<StudyGroup>
            {
                new StudyGroup{CourseID=1000, GroupTitle="Q-Step - 101", Students = new List<Student>()},
                new StudyGroup{CourseID=1001, GroupTitle="Q-Step - Advanced", Students = new List<Student>()},
                new StudyGroup{CourseID=1002, GroupTitle="Q-Step - Education", Students = new List<Student>()}
            };
            StudyStudyGroups.ForEach(s => context.StudyGroups.AddOrUpdate(p => p.StudyGroupID, s));
            context.SaveChanges();

            /*AddOrUpdateStudent(context, 1, 1);
            AddOrUpdateStudent(context, 2, 1);
            AddOrUpdateStudent(context, 3, 1);
            context.SaveChanges();*/

            AddOrUpdateStudyGroup(context, 1, 1);
            AddOrUpdateStudyGroup(context, 2, 1);
            AddOrUpdateStudyGroup(context, 3, 1);
            AddOrUpdateStudyGroup(context, 4, 1);
            AddOrUpdateStudyGroup(context, 5, 1);
            AddOrUpdateStudyGroup(context, 6, 1);
            AddOrUpdateStudyGroup(context, 7, 1);
            AddOrUpdateStudyGroup(context, 8, 1);
            AddOrUpdateStudyGroup(context, 1, 2);
            AddOrUpdateStudyGroup(context, 2, 2);
            AddOrUpdateStudyGroup(context, 5, 2);
            AddOrUpdateStudyGroup(context, 8, 2);

            context.SaveChanges();

            /*var attendancy = new List<Attendancy>
            {
                new Attendancy{TutorName="Gitit Kadar-Satat", Comments="Yoni called in sick",
                               LocationID=1 , StudyGroups = new List<StudyGroup>()},
                new Attendancy{TutorName="Gitit Kadar-Satat", Comments="Great lesson!",
                               LocationID=2 , StudyGroups = new List<StudyGroup>()},
                new Attendancy{TutorName="Gitit Kadar-Satat", Comments="The students enjoyed the lab",
                               LocationID=1 , StudyGroups = new List<StudyGroup>()}
            };
            attendancy.ForEach(s => context.Attendancies.AddOrUpdate(p => p.AttendancyID, s));
            context.SaveChanges();*/






        } //END OF Seed();

        void AddOrUpdateStudent(StudentContext context, int StudyGroupID, int studentID)
        {
            var stu = context.Students.SingleOrDefault(s => s.StudentID == studentID);
            var grp = stu.StudyGroups.SingleOrDefault(g => g.StudyGroupID == StudyGroupID);
            if (grp == null)
                stu.StudyGroups.Add(context.StudyGroups.Single(g => g.StudyGroupID == StudyGroupID));
        }

        void AddOrUpdateStudyGroup(StudentContext context, int studentID, int StudyGroupID)
        {
            var grp = context.StudyGroups.SingleOrDefault(g => g.StudyGroupID == StudyGroupID);
            var stu = grp.Students.SingleOrDefault(s => s.StudentID == studentID);
            if (stu == null)
            {
                grp.Students.Add(context.Students.Single(s => s.StudentID == studentID));
            }
        }
    }
}