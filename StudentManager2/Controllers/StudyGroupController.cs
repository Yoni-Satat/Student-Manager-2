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

namespace StudentManager2.Controllers
{
    public class StudyGroupController : Controller
    {
        private StudentContext db = new StudentContext();
        private readonly object x;

        // GET: StudyGroup
        public ActionResult Index(int? id, int? courseID)
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
            if (ModelState.IsValid)
            {
                db.StudyGroups.Add(studyGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studyGroup.CourseID);
            return View(studyGroup);
        }

        // GET: StudyGroup/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studyGroup.CourseID);
            return View(studyGroup);
        }

        // POST: StudyGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudyGroupID,CourseID,GroupTitle")] StudyGroup studyGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studyGroup.CourseID);
            return View(studyGroup);
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
