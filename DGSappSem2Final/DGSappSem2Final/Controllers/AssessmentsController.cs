using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Assements;

namespace DGSappSem2Final.Controllers
{
    public class AssessmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assessments
        public ActionResult Index()
        {
            var assessments = db.Assessments.Include(a => a.Grade);
            return View(assessments.ToList());
        }

        // GET: Assessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // GET: Assessments/Create
        public ActionResult Create()
        {
            Dictionary<int, string> gradeCollection = GetGradeNameComboCollection();
            Dictionary<int, string> termCollection = GetTermNameComboCollection();
            var asses = new Assessment
            {
                GradeNameCollection = gradeCollection.Values.ToList(),
                TermCollection = termCollection.Values.ToList(),
            };


            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName");
            return View(asses);
        }

        private Dictionary<int, string> GetGradeNameComboCollection()
        {
            var gradeCollection = new Dictionary<int, string>();

            var collection = db.Grades.ToList();

            foreach (var entry in collection)
            {
                    gradeCollection.Add(entry.GradeId, entry.GradeName);
              
            }

            return gradeCollection;
        }

        private Dictionary<int, string> GetTermNameComboCollection()
        {
            var result = new Dictionary<int, string>();

            var collection = db.Terms.ToList();

            foreach (var entry in collection)
            {

                    result.Add(entry.TermId, entry.TermName);

            }

            return result;
        }

        // POST: Assessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssessmentID,GradeId,GradeName,AssessmentDate,Term,Type")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Assessments.Add(assessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", assessment.GradeId);
            return View(assessment);
        }

        // GET: Assessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", assessment.GradeId);
            return View(assessment);
        }

        // POST: Assessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssessmentID,GradeId,GradeName,AssessmentDate,Term,Type")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", assessment.GradeId);
            return View(assessment);
        }

        // GET: Assessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            db.Assessments.Remove(assessment);
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
