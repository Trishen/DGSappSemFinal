using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Student;

namespace DGSappSem2Final.Controllers
{
    public class StudentFeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentFees
        public ActionResult Index()
        {
            var Students = db.Students.ToList();
            var studentFees = db.StudentFees.ToList();
            var gradeFees = db.Fees.ToList();
            foreach (var s in Students)
            {
                var entryExists = studentFees.Any(x => x.StudentName == s.StudentName
                && x.GradeName == s.StudentGrade);

             
                if (!entryExists)
                {
                    var fee = gradeFees.Where(x => x.GradeName == s.StudentGrade).Select(x => x.GradeFee).FirstOrDefault();

                    db.StudentFees.Add(new StudentFees { 
                    StudentName = s.StudentName,
                    GradeName = s.StudentGrade,
                    GradeFee = fee,
                    FeeBalance = GetStudentFeeBalance(s.StID, s.StudentGrade)});
                    db.SaveChanges();
                }
            }

            return View(db.StudentFees.ToList());
        }

        private double GetStudentFeeBalance(int id, string gradeName)
        {
            StudentFees studentFees = db.StudentFees.Find(id);
            var gradeFees = db.Fees.ToList();
            var fee = gradeFees.Where(x => x.GradeName == gradeName).Select(x => x.GradeFee).FirstOrDefault();

            return 0;
        }

        // GET: StudentFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFees studentFees = db.StudentFees.Find(id);
            if (studentFees == null)
            {
                return HttpNotFound();
            }
            return View(studentFees);
        }

        // GET: StudentFees/Create
        public ActionResult Create()
        {
            ViewBag.FeeId = new SelectList(db.Fees, "FeeId", "GradeName");
            return View();
        }

        // POST: StudentFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentFeeId,FeeId,GradeFee,FeeBalance")] StudentFees studentFees)
        {
            if (ModelState.IsValid)
            {
                db.StudentFees.Add(studentFees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FeeId = new SelectList(db.Fees, "FeeId", "GradeName", studentFees.FeeId);
            return View(studentFees);
        }

        // GET: StudentFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFees studentFees = db.StudentFees.Find(id);
            if (studentFees == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeeId = new SelectList(db.Fees, "FeeId", "GradeName", studentFees.FeeId);
            return View(studentFees);
        }

        // POST: StudentFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentFeeId,FeeId,GradeFee,FeeBalance")] StudentFees studentFees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentFees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FeeId = new SelectList(db.Fees, "FeeId", "GradeName", studentFees.FeeId);
            return View(studentFees);
        }

        // GET: StudentFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFees studentFees = db.StudentFees.Find(id);
            if (studentFees == null)
            {
                return HttpNotFound();
            }
            return View(studentFees);
        }

        // POST: StudentFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentFees studentFees = db.StudentFees.Find(id);
            db.StudentFees.Remove(studentFees);
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
