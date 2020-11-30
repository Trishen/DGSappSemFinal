using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Assements;
using Microsoft.WindowsAPICodePack.Shell;

namespace DGSappSem2Final.Controllers
{
    public class StudentAssessmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentAssessments
        public ActionResult Index()
        {
            var assessments = db.Assessments.ToList();
            var stuAssess = db.StudentAssessments.ToList();
            foreach (var ass in assessments)
            {

                if (!stuAssess.Any(x => x.StaffAssessment.AssessmentName == ass.AssessmentName))
                {
                    db.StudentAssessments.Add(new StudentAssessment
                    {
                        StaffAssessmentId = db.Assessments.Where(x=> x.AssessmentName == ass.AssessmentName).FirstOrDefault().AssessmentID
                    });
                    db.SaveChanges();
                }
            }

            return View(db.StudentAssessments.ToList());


        }

        // GET: StudentAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssessment studentAssessment = db.StudentAssessments.Find(id);
            if (studentAssessment == null)
            {
                return HttpNotFound();
            }
            return View(studentAssessment);
        }

        // GET: StudentAssessments/Create
        public ActionResult Create()
        {
            ViewBag.StaffAssessmentId = new SelectList(db.Assessments, "AssessmentID", "AssessmentName");
            return View();
        }

        // POST: StudentAssessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentAssessmentID,StaffAssessmentId,StaffAssessmentName")] StudentAssessment studentAssessment)
        {
            if (ModelState.IsValid)
            {
                db.StudentAssessments.Add(studentAssessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffAssessmentId = new SelectList(db.Assessments, "AssessmentID", "AssessmentName", studentAssessment.StaffAssessmentId);
            return View(studentAssessment);
        }

        // GET: StudentAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssessment studentAssessment = db.StudentAssessments.Find(id);
            if (studentAssessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffAssessmentId = new SelectList(db.Assessments, "AssessmentID", "AssessmentName", studentAssessment.StaffAssessmentId);
            return View(studentAssessment);
        }

        // POST: StudentAssessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentAssessmentID,StaffAssessmentId,StaffAssessmentName")] StudentAssessment studentAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffAssessmentId = new SelectList(db.Assessments, "AssessmentID", "AssessmentName", studentAssessment.StaffAssessmentId);
            return View(studentAssessment);
        }

        // GET: StudentAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssessment studentAssessment = db.StudentAssessments.Find(id);
            if (studentAssessment == null)
            {
                return HttpNotFound();
            }
            return View(studentAssessment);
        }

        // POST: StudentAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAssessment studentAssessment = db.StudentAssessments.Find(id);
            db.StudentAssessments.Remove(studentAssessment);
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

        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentAss = db.StudentAssessments.Find(id);
            StaffAssessment assessment = db.Assessments.Find(studentAss.StaffAssessmentId);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(studentAss);
        }

        [HttpPost]
        public ActionResult Download(int id)
        {
            var studentAss = db.StudentAssessments.Find(id);
            StaffAssessment assessment = db.Assessments.Find(studentAss.StaffAssessmentId);
            string downloadsPath = KnownFolders.Downloads.Path;

            using (Stream file = System.IO.File.OpenWrite($@"{downloadsPath}\{assessment.FileName}"))
            {
                file.Write(assessment.File, 0, assessment.File.Length);
            }
            return RedirectToAction("Index");
        }
    }
}
