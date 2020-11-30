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

namespace DGSappSem2Final.Controllers
{
    public class StaffAssessmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaffAssessments
        public ActionResult Index()
        {
            var assessments = db.Assessments.Include(s => s.Grade);
            return View(assessments.ToList());
        }

        // GET: StaffAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAssessment staffAssessment = db.Assessments.Find(id);
            if (staffAssessment == null)
            {
                return HttpNotFound();
            }
            return View(staffAssessment);
        }

        // GET: StaffAssessments/Create
        public ActionResult Create()
        {
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName");
            return View(new StaffAssessment());
        }

        // POST: StaffAssessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssessmentID,AssessmentName,GradeId,GradeName,FileName,FileType,File,DownloadPath")] StaffAssessment staffAssessment, HttpPostedFileBase files)
        {
            staffAssessment.FileName = files.FileName;
            staffAssessment.FileType = files.ContentType;

            using (Stream inputStream = files.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                staffAssessment.File = memoryStream.ToArray();
            }

            if (ModelState.IsValid)
            {
                db.Assessments.Add(staffAssessment);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: StaffAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAssessment staffAssessment = db.Assessments.Find(id);
            if (staffAssessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", staffAssessment.GradeId);
            return View(staffAssessment);
        }

        // POST: StaffAssessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssessmentID,AssessmentName,GradeId,GradeName,FileName,FileType,File,DownloadPath")] StaffAssessment staffAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", staffAssessment.GradeId);
            return View(staffAssessment);
        }

        // GET: StaffAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAssessment staffAssessment = db.Assessments.Find(id);
            if (staffAssessment == null)
            {
                return HttpNotFound();
            }
            return View(staffAssessment);
        }

        // POST: StaffAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffAssessment staffAssessment = db.Assessments.Find(id);
            db.Assessments.Remove(staffAssessment);
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
