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
        public ActionResult Edit(int? id)
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
            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle", attendanceRecord.StudyGroupID);
            return View(attendanceRecord);
        }

        // POST: AttendanceRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceRecordID,StudyGroupID,TutorName,Notes,Date,Time,LocationID")] AttendanceRecord attendanceRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendanceRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudyGroupID = new SelectList(db.StudyGroups, "StudyGroupID", "GroupTitle", attendanceRecord.StudyGroupID);
            return View(attendanceRecord);
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
