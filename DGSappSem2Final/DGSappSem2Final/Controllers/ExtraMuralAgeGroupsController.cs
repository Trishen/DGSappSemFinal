﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Murals;

namespace DGSappSem2Final.Controllers
{
    public class ExtraMuralAgeGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraMuralAgeGroups
        public ActionResult Index()
        {
            var extraMuralAgeGroups = db.ExtraMuralAgeGroups.Include(e => e.ExtraMural).Include(e => e.Staff);
            return View(extraMuralAgeGroups.ToList());
        }

        // GET: ExtraMuralAgeGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraMuralAgeGroups extraMuralAgeGroups = db.ExtraMuralAgeGroups.Find(id);
            if (extraMuralAgeGroups == null)
            {
                return HttpNotFound();
            }
            return View(extraMuralAgeGroups);
        }

        // GET: ExtraMuralAgeGroups/Create
        public ActionResult Create()
        {
            ViewBag.ExtraMuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName");
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title");
            return View();
        }

        // POST: ExtraMuralAgeGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MuralAgeGroupId,MuralName,ExtraMuralId,ExtraMuralName,AgeGroupName,NoAssignedStudents,StaffId,AssignedTeacher")] ExtraMuralAgeGroups extraMuralAgeGroups)
        {
            if (ModelState.IsValid)
            {
                db.ExtraMuralAgeGroups.Add(extraMuralAgeGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExtraMuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName", extraMuralAgeGroups.ExtraMuralId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", extraMuralAgeGroups.StaffId);
            return View(extraMuralAgeGroups);
        }

        // GET: ExtraMuralAgeGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraMuralAgeGroups extraMuralAgeGroups = db.ExtraMuralAgeGroups.Find(id);
            if (extraMuralAgeGroups == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExtraMuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName", extraMuralAgeGroups.ExtraMuralId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", extraMuralAgeGroups.StaffId);
            return View(extraMuralAgeGroups);
        }

        // POST: ExtraMuralAgeGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MuralAgeGroupId,MuralName,ExtraMuralId,ExtraMuralName,AgeGroupName,NoAssignedStudents,StaffId,AssignedTeacher")] ExtraMuralAgeGroups extraMuralAgeGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraMuralAgeGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExtraMuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName", extraMuralAgeGroups.ExtraMuralId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", extraMuralAgeGroups.StaffId);
            return View(extraMuralAgeGroups);
        }

        // GET: ExtraMuralAgeGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraMuralAgeGroups extraMuralAgeGroups = db.ExtraMuralAgeGroups.Find(id);
            if (extraMuralAgeGroups == null)
            {
                return HttpNotFound();
            }
            return View(extraMuralAgeGroups);
        }

        // POST: ExtraMuralAgeGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraMuralAgeGroups extraMuralAgeGroups = db.ExtraMuralAgeGroups.Find(id);
            db.ExtraMuralAgeGroups.Remove(extraMuralAgeGroups);
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
