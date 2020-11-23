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
    public class FeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fees
        public ActionResult Index()
        {
            var fees = db.Fees.Include(f => f.Grade);
            return View(fees.ToList());
        }

        // GET: Fees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: Fees/Create
        public ActionResult Create()
        {
            Dictionary<int, string> gradeCollection = GetGradeNameComboCollection();
            var fees = new Fee
            {
                GradeNameCollection = gradeCollection.Values.ToList(),
            };


            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName");
            return View(fees);
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

        // POST: Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeeId,GradeId,GradeName,GradeFee")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Fees.Add(fee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", fee.GradeId);
            return View(fee);
        }

        // GET: Fees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", fee.GradeId);
            return View(fee);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeeId,GradeId,GradeName,GradeFee")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", fee.GradeId);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fee fee = db.Fees.Find(id);
            db.Fees.Remove(fee);
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
