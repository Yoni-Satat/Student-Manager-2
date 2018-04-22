using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManager2.DAL;
using StudentManager2.Models;
using StudentManager2.ViewModels;
using System.Data.Entity.Infrastructure;

namespace StudentManager2.Controllers
{
    public class StudyGroupController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: StudyGroup
        public ActionResult Index(int? id, int? studentID)
        {
            var viewModel = new GroupIndexData();
            viewModel.StudyGroups = db.StudyGroups
                .Include(s => s.Course)
                .Include(s => s.Course.Lessons)
                .Include(s => s.Students);
            if (id != null)
            {
                ViewBag.StudyGroupID = id.Value;
                viewModel.Students = viewModel.StudyGroups.Where(
                    s => s.StudyGroupID == id.Value).Single().Students;
            }

            return View(viewModel);
        }

        // GET: StudyGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyGroup studyGroup = db.StudyGroups.Find(id);
            if (studyGroup == null)
            {
                return HttpNotFound();
            }
            return View(studyGroup);
        }

        // GET: StudyGroup/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            return View();
        }

        // POST: StudyGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudyGroupID,CourseID,GroupTitle")] StudyGroup studyGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.StudyGroups.Add(studyGroup);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studyGroup.CourseID);
            PopulateStudentsList(studyGroup);
            return View(studyGroup);
        }

        // GET: StudyGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyGroup studyGroup = db.StudyGroups
                .Include(s => s.Students)
                .Where(s => s.StudyGroupID == id).Single();
                PopulateAddStudentToGroup(studyGroup);
            if (studyGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studyGroup.CourseID);
            return View(studyGroup);
        }

        private void PopulateAddStudentToGroup(StudyGroup studyGroup)
        {
            var allStudents = db.Students;
            var studyGroupStudents = new HashSet<int>(studyGroup.Students.Select(s => s.StudentID));
            var viewModel = new List<AddStudentToGroup>();
            foreach (var student in allStudents)
            {
                viewModel.Add(new AddStudentToGroup
                {
                    StudentID = student.StudentID,
                    FullName = student.FullName,
                    AddStudent = studyGroupStudents.Contains(student.StudentID)
                });
            }
            ViewBag.Students = viewModel;
        }

        // POST: StudyGroup/Edit/5
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
            var groupToUpdate = db.StudyGroups
               .Include(s => s.Students)
               .Where(s => s.StudyGroupID == id)
               .Single();

            if (TryUpdateModel(groupToUpdate, "",
                new string[] { "GroupTitle" }))
            {
                try
                {

                    UpdateGroupStudents(selectedStudents, groupToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAddStudentToGroup(groupToUpdate);
            return View(groupToUpdate);
        }

        private void UpdateGroupStudents(string[] selectedStudents, StudyGroup groupToUpdate)
        {
            if (selectedStudents == null)
            {
                groupToUpdate.Students = new List<Student>();
                return;
            }

            var selectedStudentsHS = new HashSet<string>(selectedStudents);
            var groupStudents = new HashSet<int>
                (groupToUpdate.Students.Select(s => s.StudentID));
            foreach (var student in db.Students)
            {
                if (selectedStudentsHS.Contains(student.StudentID.ToString()))
                {
                    if (!groupStudents.Contains(student.StudentID))
                    {
                        groupToUpdate.Students.Add(student);
                    }
                }
                else
                {
                    if (groupStudents.Contains(student.StudentID))
                    {
                        groupToUpdate.Students.Remove(student);
                    }
                }
            }
        }



        private void PopulateStudentsList(object selectedStudent = null)
        {
            var studentsQuery = from s in db.Students
                                orderby s.FirstName
                                select s;
            ViewBag.StudentID = new SelectList(studentsQuery, "StudentID", "FirstName", selectedStudent);
        }

        // GET: StudyGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyGroup studyGroup = db.StudyGroups.Find(id);
            if (studyGroup == null)
            {
                return HttpNotFound();
            }
            return View(studyGroup);
        }

        // POST: StudyGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudyGroup studyGroup = db.StudyGroups.Find(id);
            db.StudyGroups.Remove(studyGroup);
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
