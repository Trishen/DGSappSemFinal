using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;

namespace DGSappSem2Final.Controllers
{
    public class StaffSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaffSubjects
        public ActionResult Index()
        {
            var staffSubjects = db.StaffSubjects.Include(s => s.GradeSubject).Include(s => s.Staff).Include(s => s.Subject);
            var gradeSubject = db.GradeSubjects.ToList();
            foreach (var gsub in gradeSubject)
            {
                var subject = db.Subjects.Find(gsub.SubjectId);
                var grade = db.Grades.Find(gsub.GradeId);

                var entryExists = db.StaffSubjects.Any(x => x.GradeName == grade.GradeName && x.SubjectName == subject.SubjectName);
                        
                if (!entryExists)
                {
                    db.StaffSubjects.Add(new StaffSubjects { 
                        //StaffSubjectId = GetMaxStaffSubjectId() + 1,
                    SubjectName = subject.SubjectName,
                    GradeName = grade.GradeName,
                    });;

                    db.SaveChanges();
                }
            }

            var stsub = db.StaffSubjects.ToList();
            return View(stsub);
        }

        private  int GetMaxStaffSubjectId()
        {
            return db.StaffSubjects.Max(x => x.StaffSubjectId);
        }

        // GET: StaffSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSubjects staffSubjects = db.StaffSubjects.Find(id);
            if (staffSubjects == null)
            {
                return HttpNotFound();
            }
            return View(staffSubjects);
        }

        // GET: StaffSubjects/Create
        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.GradeSubjects, "GradeSubjectId", "GradeName");
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            var staffSubjects = new StaffSubjects();
            Dictionary<int, string> teacherCollection = GetTeacherNameComboCollection();

            staffSubjects.TeacherNameCollection = teacherCollection.Values.ToList();

            return View(staffSubjects);
        }

        // POST: StaffSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffSubjectId,GradeSubjectId,GradeName,SubjectId,SubjectName,StaffId,AssignedTeacher")] StaffSubjects staffSubjects)
        {
            Dictionary<int, string> teacherCollection = GetTeacherNameComboCollection();

            staffSubjects.TeacherNameCollection = teacherCollection.Values.ToList();

            if (ModelState.IsValid)
            {
                db.StaffSubjects.Add(staffSubjects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.GradeSubjects, "GradeSubjectId", "GradeName", staffSubjects.SubjectId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", staffSubjects.StaffId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", staffSubjects.SubjectId);
            return View(staffSubjects);
        }

        private Dictionary<int, string> GetTeacherNameComboCollection()
        {
            var teacherCollection = new Dictionary<int, string>();

            var collection = db.Staffs.ToList();

            foreach (var entry in collection)
            {
                var displayName = $"{entry.Title}. {entry.Name} {entry.Surname}";
                var subjectsAvailable = db.StaffSubjects.Count(x => x.AssignedTeacher.Equals(displayName)) < 4;

                if (entry.StaffPositionName != "Principle" && entry.StaffPositionName != "Vice Principle" && subjectsAvailable)
                {
                    teacherCollection.Add(entry.StaffId, displayName);
                }
            }

            return teacherCollection;
        }

        private Dictionary<int?, string> GetSubjectNameCollection(string subjectName)
        {
            var subjectCollection = new Dictionary<int?, string>();

            var collection = db.GradeSubjects.ToList();

            foreach (var entry in collection.Where(x => x.GradeName.Equals(subjectName)).ToList())
            {
                subjectCollection.Add(entry.GradeId, entry.GradeName);
            }

            return subjectCollection;
        }


        // GET: StaffSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            Dictionary<int, string> teacherCollection = GetTeacherNameComboCollection();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSubjects staffSubjects = db.StaffSubjects.Find(id);
            if (staffSubjects == null)
            {
                return HttpNotFound();
            }

            staffSubjects.TeacherNameCollection = teacherCollection.Values.ToList();
            ViewBag.SubjectId = new SelectList(db.GradeSubjects, "GradeSubjectId", "GradeName", staffSubjects.SubjectId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", staffSubjects.StaffId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", staffSubjects.SubjectId);
            return View(staffSubjects);
        }

        // POST: StaffSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffSubjectId,GradeSubjectId,GradeName,SubjectId,SubjectName,StaffId,AssignedTeacher")] StaffSubjects staffSubjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffSubjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.GradeSubjects, "GradeSubjectId", "GradeName", staffSubjects.SubjectId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", staffSubjects.StaffId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", staffSubjects.SubjectId);
            return View(staffSubjects);
        }

        // GET: StaffSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSubjects staffSubjects = db.StaffSubjects.Find(id);
            if (staffSubjects == null)
            {
                return HttpNotFound();
            }
            return View(staffSubjects);
        }

        // POST: StaffSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffSubjects staffSubjects = db.StaffSubjects.Find(id);
            db.StaffSubjects.Remove(staffSubjects);
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
