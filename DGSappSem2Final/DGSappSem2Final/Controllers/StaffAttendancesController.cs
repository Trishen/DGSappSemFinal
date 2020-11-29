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
    public class StaffAttendancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaffAttendances
        public ActionResult Index()
        {
            return View(db.StaffAttendances.ToList());
        }

        // GET: StaffAttendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAttendance staffAttendance = db.StaffAttendances.Find(id);
            if (staffAttendance == null)
            {
                return HttpNotFound();
            }
            return View(staffAttendance);
        }

        // GET: StaffAttendances/Create
        public ActionResult Create()
        {
            return View(new StaffAttendance());
        }

        // POST: StaffAttendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "StaffAttendanceId,StaffAttName,Staffrecord,GetDate")] StaffAttendance staffAttendance)
        {
            if (ModelState.IsValid)
            {
                db.StaffAttendances.Add(staffAttendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffAttendance);
        }

        // GET: StaffAttendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAttendance staffAttendance = db.StaffAttendances.Find(id);
            if (staffAttendance == null)
            {
                return HttpNotFound();
            }
            return View(staffAttendance);
        }

        // POST: StaffAttendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffAttendanceId,StaffAttName,Staffrecord,GetDate")] StaffAttendance staffAttendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffAttendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffAttendance);
        }

        // GET: StaffAttendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAttendance staffAttendance = db.StaffAttendances.Find(id);
            if (staffAttendance == null)
            {
                return HttpNotFound();
            }
            return View(staffAttendance);
        }

        // POST: StaffAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffAttendance staffAttendance = db.StaffAttendances.Find(id);
            db.StaffAttendances.Remove(staffAttendance);
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
