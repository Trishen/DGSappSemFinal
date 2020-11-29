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
using Nexmo.Api;

namespace DGSappSem2Final.Controllers
{
    public class StudentFeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentFees
        public ActionResult Index()
        {
            var students = db.Students.ToList();
            var studentFees = db.StudentFees.ToList();
            foreach (var student in students)
            {
                var fee = db.Fees.Where(x => x.GradeName == student.StudentGrade).FirstOrDefault().GradeFee;

                if (!studentFees.Any(x => x.StudentName == student.StudentName
                 && x.GuardianName == student.ParentName
                 && x.GuardianContact == student.ParentContact
                 && x.GradeName == student.StudentGrade
                 && x.GradeFee == fee))
                {
                    db.StudentFees.Add(new StudentFees
                    {
                        StudentName = student.StudentName,
                        GuardianName = student.ParentName,
                        GuardianContact = student.ParentContact,
                        GradeName = student.StudentGrade,
                        GradeFee = fee,
                        FeeBalance = fee
                    });
                }
                db.SaveChanges();
            }

            return View(db.StudentFees.ToList());
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

        public ActionResult SMSReminder(int? id)
        {
            var message = string.Empty;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFees studentFees = db.StudentFees.Find(id);

            message = "Hi, " + studentFees.GuardianName + "\n\nPlease note outstanding Fees Balance Of: " + studentFees.FeeBalance + " is over due.\nPlease pay as soon as possible.";
            studentFees.SMSMessage = message;

            return View(studentFees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SMSReminder([Bind(Include = "StudentFeeId,FeeId,GradeFee,FeeBalance,SMSMessage")] StudentFees studentFees)
        {

            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "7692f400",
                ApiSecret = "Zua8eJBJEcveP0Zn"
            });
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = "DGS",
                to = "27817375820",
                //update to include book information 

                text = studentFees.SMSMessage
            });

            return View(studentFees);
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
