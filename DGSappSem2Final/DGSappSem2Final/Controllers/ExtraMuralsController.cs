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
    public class ExtraMuralsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraMurals
        public ActionResult Index()
        {
            return View(db.ExtraMurals.ToList());
        }

        // GET: ExtraMurals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraMural extraMural = db.ExtraMurals.Find(id);
            if (extraMural == null)
            {
                return HttpNotFound();
            }
            return View(extraMural);
        }

        // GET: ExtraMurals/Create
        public ActionResult Create()
        {
            return View(new ExtraMural());
        }

        // POST: ExtraMurals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MuralId,MuralName")] ExtraMural extraMural)
        {
            if (ModelState.IsValid)
            {
                db.ExtraMurals.Add(extraMural);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(extraMural);
        }

        // GET: ExtraMurals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraMural extraMural = db.ExtraMurals.Find(id);
            if (extraMural == null)
            {
                return HttpNotFound();
            }
            return View(extraMural);
        }

        // POST: ExtraMurals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MuralId,MuralName")] ExtraMural extraMural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraMural).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(extraMural);
        }

        // GET: ExtraMurals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraMural extraMural = db.ExtraMurals.Find(id);
            if (extraMural == null)
            {
                return HttpNotFound();
            }
            return View(extraMural);
        }

        // POST: ExtraMurals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraMural extraMural = db.ExtraMurals.Find(id);
            db.ExtraMurals.Remove(extraMural);
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
