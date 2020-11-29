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
    public class Assessments2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assessments2
        public ActionResult Index()
        {
            return View(db.Assessments.ToList());
        }

        // GET: Assessments2/Details/5
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

        // GET: Assessments2/Create
        public ActionResult Create()
        {
            return View(new Assessment());
        }

        // POST: Assessments2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssessmentID,AssessmentName")] Assessment assessment, HttpPostedFileBase files)
        {
            assessment.FileName = files.FileName;
            assessment.FileType = files.ContentType;

            using (Stream inputStream = files.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                assessment.File = memoryStream.ToArray();
            }

            if (ModelState.IsValid)
            {
                db.Assessments.Add(assessment);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Assessments2/Edit/5
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
            return View(assessment);
        }

        // POST: Assessments2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssessmentID,AssessmentName")] Assessment assessment, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessment);
        }

        // GET: Assessments2/Delete/5
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

        // POST: Assessments2/Delete/5
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

        public ActionResult Download(int? id)
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

        [HttpPost]
        public ActionResult Download(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            string downloadsPath = KnownFolders.Downloads.Path;

            //using (Stream file = System.IO.File.OpenWrite($@"{downloadsPath}\{assessment.FileName}"))
            //{
            //    file.Write(assessment.File, 0, assessment.File.Length);
            //}

            assessment.DownloadPath = downloadsPath;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
