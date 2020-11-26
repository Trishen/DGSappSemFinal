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
using System.Threading.Tasks;

namespace DGSappSem2Final.Controllers
{
    public class AssessmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assessments
        public ActionResult Index()
        {
            return View(db.Assessments.ToList());
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
            return View();
        }

        // POST: Assessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssessmentID,Grade,StartTime,EndTime,AssessmentDate,AssessmentVenue,Term,Type")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Assessments.Add(assessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(assessment);
        }

        // POST: Assessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssessmentID,Grade,StartTime,EndTime,AssessmentDate,AssessmentVenue,Term,Type")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
        //public async Task<ActionResult> assessmentAccess()
        //{
        //    return View(await db.Assessments.ToListAsync());
        //}

    }
}
