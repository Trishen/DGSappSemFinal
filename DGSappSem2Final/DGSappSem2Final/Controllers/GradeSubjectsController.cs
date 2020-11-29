using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Grade;

namespace DGSappSem2Final.Controllers
{
    public class GradeSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GradeSubjects
        public ActionResult Index()
        {     
            var gradeSubjects = db.GradeSubjects.Include(g => g.Grade).Include(g => g.Subject);

            return View(gradeSubjects.ToList());
        }

        // GET: GradeSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeSubjects gradeSubjects = db.GradeSubjects.Find(id);
            if (gradeSubjects == null)
            {
                return HttpNotFound();
            }
            return View(gradeSubjects);
        }

        // GET: GradeSubjects/Create
        public ActionResult Create()
        {
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            return View(new GradeSubjects());
        }

        // POST: GradeSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeSubjectId,GradeId,GradeName,SubjectId,NoOfLessonsRequired")] GradeSubjects gradeSubjects)
        {

            gradeSubjects.GradeName = db.Grades.Find(gradeSubjects.GradeId).GradeName;
            gradeSubjects.SubjectName = db.Subjects.Find(gradeSubjects.SubjectId).SubjectName;

 
            if (ModelState.IsValid)
            {
                db.GradeSubjects.Add(gradeSubjects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", gradeSubjects.GradeId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", gradeSubjects.SubjectId);
            return View(gradeSubjects);
        }

        // GET: GradeSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeSubjects gradeSubjects = db.GradeSubjects.Find(id);
            if (gradeSubjects == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", gradeSubjects.GradeId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", gradeSubjects.SubjectId);
            return View(gradeSubjects);
        }

        // POST: GradeSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeSubjectId,GradeId,GradeName,SubjectId,NoOfLessonsRequired")] GradeSubjects gradeSubjects)
        {

            if (ModelState.IsValid)
            {
                db.Entry(gradeSubjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", gradeSubjects.GradeId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", gradeSubjects.SubjectId);
            return View(gradeSubjects);
        }

        // GET: GradeSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeSubjects gradeSubjects = db.GradeSubjects.Find(id);
            if (gradeSubjects == null)
            {
                return HttpNotFound();
            }
            return View(gradeSubjects);
        }

        // POST: GradeSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradeSubjects gradeSubjects = db.GradeSubjects.Find(id);
            db.GradeSubjects.Remove(gradeSubjects);
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
