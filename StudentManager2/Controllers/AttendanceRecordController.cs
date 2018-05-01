using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManager2.DAL;
using StudentManager2.Models;
using StudentManager2.ViewModels;

namespace StudentManager2.Controllers
{
    public class AttendanceRecordController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: AttendanceRecord
        public ActionResult Index()
        {
            var attendanceRecords = db.AttendanceRecords
                .Include(a => a.StudyGroup)
                .Include(a => a.Location)
                .Include(a => a.StudyGroup.Course)
                .Include(a => a.Lesson);
            return View(attendanceRecords.ToList());
        }

        // GET: AttendanceRecord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceRecord attendanceRecord = db.AttendanceRecords.Find(id);
            if (attendanceRecord == null)
            {
                return HttpNotFound();
            }
            return View(attendanceRecord);
        }

        // GET: AttendanceRecord/Create
        public ActionResult Create()
        {
            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle");
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Building");
            return View();
        }

        // POST: AttendanceRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceRecordID,StudyGroupID,TutorName,Notes,Date,Time,LocationID")] AttendanceRecord attendanceRecord)
        {
            if (ModelState.IsValid)
            {
                using (var context = new StudentContext())
                {
                    var record = new AttendanceRecord
                    {
                        StudyGroupID = attendanceRecord.StudyGroupID,
                        TutorName = attendanceRecord.TutorName,
                        Notes = attendanceRecord.Notes,
                        Date = DateTime.Now,
                        Time = DateTime.Now,
                        LocationID = attendanceRecord.LocationID
                    };
                    context.Entry(record).State = EntityState.Added;
                    context.SaveChanges();
                }
                
                //db.AttendanceRecords.Add(attendanceRecord);
                //db.Entry(attendanceRecord).State = EntityState.Added;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle", attendanceRecord.StudyGroupID);
            ViewBag.LocattionID = new SelectList(db.Locations, "LocationID", "Building", attendanceRecord.LocationID);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AttendanceRecord attendanceRecord = db.AttendanceRecords
                .Include(ar => ar.Location)
                .Include(ar => ar.StudyGroup.Course)
                .Include(ar => ar.Lesson)
                .Include(ar => ar.StudyGroup)
                .Include(ar => ar.Students).Where(ar => ar.AttendanceRecordID == id).Single();
            PopulateStudentAttendanceRecord(attendanceRecord);

            if (attendanceRecord == null)
            {
                return HttpNotFound();
            }
            var lessonID = db.Lessons.Where(l => l.CourseID == attendanceRecord.StudyGroup.CourseID);
            ViewBag.CourseID = attendanceRecord.StudyGroup.CourseID;
            ViewBag.LessonID = new SelectList(lessonID, "LessonID", "Topic", attendanceRecord.LessonID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Building", attendanceRecord.LocationID);
            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle", attendanceRecord.StudyGroupID);
            return View(attendanceRecord);
        }

        private void PopulateStudentAttendanceRecord(AttendanceRecord attendance)
        {
            var selectedGroup = db.StudyGroups.Where(sg => sg.StudyGroupID == attendance.StudyGroupID).Single();            
            var studyGroupStudents = new HashSet<int>(attendance.Students.Select(s => s.StudentID));
            db.Entry(selectedGroup).Collection(sg => sg.Students).Load();
            var viewModel = new List<StudentsAttendanceRecord>();
            foreach (Student student in selectedGroup.Students)
            {
                viewModel.Add(new StudentsAttendanceRecord
                {
                    StudentID = student.StudentID,
                    FullName = student.FullName,
                    AddStudent = studyGroupStudents.Contains(student.StudentID)
                });
            }
            ViewBag.Students = viewModel;
        }

        


        // POST: AttendanceRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedStudents)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var recordToUpdate = db.AttendanceRecords
               .Include(s => s.Location)
               .Include(s => s.Students)
               .Where(s => s.AttendanceRecordID == id)
               .Single();
            if (TryUpdateModel(recordToUpdate, "",
                new string[] { "StudyGroupID", "LocationID", "TutorName", "Notes", "Date", "Time", "LessonID" }))
            {
                try
                {

                    UpdateAttendanceRecord(selectedStudents, recordToUpdate);
                    var selectedRecord = db.AttendanceRecords.Where(a => a.AttendanceRecordID == id).Single();
                    selectedRecord.Date = DateTime.Now;
                    selectedRecord.Time = DateTime.Now;
                    db.Entry(selectedRecord).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateStudentAttendanceRecord(recordToUpdate);
            return View(recordToUpdate);
        }

        private void UpdateAttendanceRecord(string[] selectedStudents, AttendanceRecord recordToUpdate)
        {
            if (selectedStudents == null)
            {
                recordToUpdate.Students = new List<Student>();
                return;
            }

            var selectedStudentsHS = new HashSet<string>(selectedStudents);
            var groupStudents = new HashSet<int>
                (recordToUpdate.Students.Select(s => s.StudentID));
            foreach (var student in db.Students)
            {
                if (selectedStudentsHS.Contains(student.StudentID.ToString()))
                {
                    if (!groupStudents.Contains(student.StudentID))
                    {
                        recordToUpdate.Students.Add(student);
                    }
                }
                else
                {
                    if (groupStudents.Contains(student.StudentID))
                    {
                        recordToUpdate.Students.Remove(student);
                    }
                }
            }
        }

        // GET: AttendanceRecord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceRecord attendanceRecord = db.AttendanceRecords.Find(id);
            if (attendanceRecord == null)
            {
                return HttpNotFound();
            }
            return View(attendanceRecord);
        }

        // POST: AttendanceRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttendanceRecord attendanceRecord = db.AttendanceRecords.Find(id);
            db.AttendanceRecords.Remove(attendanceRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
