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
    public class StaffPositionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaffPositions
        public ActionResult Index()
        {
            return View(db.StaffPositions.ToList());
        }

        // GET: StaffPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffPositions staffPositions = db.StaffPositions.Find(id);
            if (staffPositions == null)
            {
                return HttpNotFound();
            }
            return View(staffPositions);
        }

        // GET: StaffPositions/Create
        public ActionResult Create()
        {
            return View(new StaffPositions());
        }

        // POST: StaffPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffPositionId,StaffPositionName,LimitedPosition,Limit")] StaffPositions staffPositions)
        {
            if (ModelState.IsValid)
            {
                db.StaffPositions.Add(staffPositions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffPositions);
        }

        // GET: StaffPositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffPositions staffPositions = db.StaffPositions.Find(id);
            if (staffPositions == null)
            {
                return HttpNotFound();
            }
            return View(staffPositions);
        }

        // POST: StaffPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffPositionId,StaffPositionName,LimitedPosition,Limit")] StaffPositions staffPositions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffPositions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffPositions);
        }

        // GET: StaffPositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffPositions staffPositions = db.StaffPositions.Find(id);
            if (staffPositions == null)
            {
                return HttpNotFound();
            }
            return View(staffPositions);
        }

        // POST: StaffPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffPositions staffPositions = db.StaffPositions.Find(id);
            db.StaffPositions.Remove(staffPositions);
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
