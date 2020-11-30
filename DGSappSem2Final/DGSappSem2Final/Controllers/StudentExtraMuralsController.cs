using System;
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
    public class StudentExtraMuralsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentExtraMurals
        public ActionResult Index()
        {
            var studentExtraMurals = db.StudentExtraMurals.Include(s => s.ExtraMural).Include(s => s.ExtraMuralAgeGroup).Include(s => s.Student);
            return View(studentExtraMurals.ToList());
        }

        // GET: StudentExtraMurals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExtraMural studentExtraMural = db.StudentExtraMurals.Find(id);
            if (studentExtraMural == null)
            {
                return HttpNotFound();
            }
            return View(studentExtraMural);
        }

        // GET: StudentExtraMurals/Create
        public ActionResult Create()
        {
            ViewBag.MuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName");
            ViewBag.MuralAgeGroupId = new SelectList(db.ExtraMuralAgeGroups, "MuralAgeGroupId", "ExtraMuralName");
            ViewBag.StudentId = new SelectList(db.Students, "StID", "StudentName");
            return View();
        }

        // POST: StudentExtraMurals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentMuralId,StudentId,MuralAgeGroupId,MuralId")] StudentExtraMural studentExtraMural)
        {
            if (ModelState.IsValid)
            {
                db.StudentExtraMurals.Add(studentExtraMural);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName", studentExtraMural.MuralId);
            ViewBag.MuralAgeGroupId = new SelectList(db.ExtraMuralAgeGroups, "MuralAgeGroupId", "ExtraMuralName", studentExtraMural.MuralAgeGroupId);
            ViewBag.StudentId = new SelectList(db.Students, "StID", "StudentName", studentExtraMural.StudentId);
            return View(studentExtraMural);
        }

        // GET: StudentExtraMurals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExtraMural studentExtraMural = db.StudentExtraMurals.Find(id);
            if (studentExtraMural == null)
            {
                return HttpNotFound();
            }
            ViewBag.MuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName", studentExtraMural.MuralId);
            ViewBag.MuralAgeGroupId = new SelectList(db.ExtraMuralAgeGroups, "MuralAgeGroupId", "ExtraMuralName", studentExtraMural.MuralAgeGroupId);
            ViewBag.StudentId = new SelectList(db.Students, "StID", "StudentName", studentExtraMural.StudentId);
            return View(studentExtraMural);
        }

        // POST: StudentExtraMurals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentMuralId,StudentId,MuralAgeGroupId,MuralId")] StudentExtraMural studentExtraMural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentExtraMural).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MuralId = new SelectList(db.ExtraMurals, "MuralId", "MuralName", studentExtraMural.MuralId);
            ViewBag.MuralAgeGroupId = new SelectList(db.ExtraMuralAgeGroups, "MuralAgeGroupId", "ExtraMuralName", studentExtraMural.MuralAgeGroupId);
            ViewBag.StudentId = new SelectList(db.Students, "StID", "StudentName", studentExtraMural.StudentId);
            return View(studentExtraMural);
        }

        // GET: StudentExtraMurals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExtraMural studentExtraMural = db.StudentExtraMurals.Find(id);
            if (studentExtraMural == null)
            {
                return HttpNotFound();
            }
            return View(studentExtraMural);
        }

        // POST: StudentExtraMurals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentExtraMural studentExtraMural = db.StudentExtraMurals.Find(id);
            db.StudentExtraMurals.Remove(studentExtraMural);
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
