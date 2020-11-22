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
    public class StaffTimetablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaffTimetables
        public ActionResult Index()
        {
            var staffTimetableList = db.StaffTimetables.ToList();

            var staff = db.Staffs.ToList();
            foreach (var s in staff)
            {
                var displayName = $"{s.Title}. {s.Name} {s.Surname}";
                var entryExists = db.StaffTimetables.Any(x => x.AssignedTeacher == displayName);

                if (!entryExists)
                {
                    db.StaffTimetables.Add(new StaffTimetable
                    {
                        AssignedTeacher = displayName
                    }); ;

                    db.SaveChanges();
                }
            }


            return View(db.StaffTimetables.ToList());
        }

        // GET: StaffTimetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTimetable staffTimetable = db.StaffTimetables.Find(id);
            if (staffTimetable == null)
            {
                return HttpNotFound();
            }
            return View(staffTimetable);
        }

        // GET: StaffTimetables/Create
        public ActionResult Create()
        {
            return View(new StaffTimetable());
        }

        // POST: StaffTimetables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffTimetableId")] StaffTimetable staffTimetable)
        {
            if (ModelState.IsValid)
            {
                db.StaffTimetables.Add(staffTimetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffTimetable);
        }

        // GET: StaffTimetables/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTimetable staffTimetable = db.StaffTimetables.Find(id);

            var reg = db.Classes.ToList();
            var assignedReg = reg.Where(x => x.AssignedTeacher == staffTimetable.AssignedTeacher).ToList();

            var staffSubs = db.StaffSubjects.ToList();
            var assignedSubs = staffSubs.Where(x=> x.AssignedTeacher == staffTimetable.AssignedTeacher).ToList();

            if (assignedSubs.Count > 0 || assignedReg.Count == 1)
            {
                staffTimetable.HasAssignedClasses = true;
            }
            else
            {
                staffTimetable.HasAssignedClasses = false;
            }

            if (assignedReg.Count == 1)
            {
                staffTimetable.Registration = assignedReg.FirstOrDefault().ClassName;
            }



            if (staffTimetable == null)
            {
                return HttpNotFound();
            }
            return View(staffTimetable);
        }

        // POST: StaffTimetables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffTimetableId")] StaffTimetable staffTimetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffTimetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffTimetable);
        }

        // GET: StaffTimetables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTimetable staffTimetable = db.StaffTimetables.Find(id);
            if (staffTimetable == null)
            {
                return HttpNotFound();
            }
            return View(staffTimetable);
        }

        // POST: StaffTimetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffTimetable staffTimetable = db.StaffTimetables.Find(id);
            db.StaffTimetables.Remove(staffTimetable);
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
