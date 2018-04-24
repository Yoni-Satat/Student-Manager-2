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
            var attendanceRecords = db.AttendanceRecords.Include(a => a.StudyGroup);
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
                db.AttendanceRecords.Add(attendanceRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle", attendanceRecord.StudyGroupID);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecord/Edit/5
        public ActionResult Edit(int? id, int? studyGroupID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AttendanceRecord attendanceRecord = db.AttendanceRecords.Include(ar => ar.Students)
                .Where(ar => ar.AttendanceRecordID == id).Single();
            PopulateStudentAttendanceRecord(attendanceRecord);



            if (attendanceRecord == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle", attendanceRecord.StudyGroupID);
            return View(attendanceRecord);
        }

        private void PopulateStudentAttendanceRecord(AttendanceRecord attendance)
        {
            var selectedGroup = db.StudyGroups.Where(sg => sg.StudyGroupID == attendance.StudyGroupID).Single();
            //var students = db.Students;
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
               .Include(s => s.Students)
               .Where(s => s.AttendanceRecordID == id)
               .Single();
            if (TryUpdateModel(recordToUpdate, "",
                new string[] { "GroupTitle" }))
            {
                try
                {

                    UpdateAttendanceRecord(selectedStudents, recordToUpdate);

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
