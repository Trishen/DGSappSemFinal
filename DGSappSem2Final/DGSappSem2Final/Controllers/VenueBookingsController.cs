using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Events;

namespace DGSappSem2Final.Controllers
{
    public class VenueBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueBookings
        public ActionResult Index()
        {
            var venueBookings = db.VenueBookings.Include(v => v.Class).Include(v => v.Grade).Include(v => v.Venue);
            return View(venueBookings.ToList());
        }

        // GET: VenueBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = db.VenueBookings.Find(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            return View(venueBooking);
        }

        // GET: VenueBookings/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName");
            ViewBag.VenueId = new SelectList(db.Venues, "venueId", "venueName");
            return View(new VenueBooking());
        }

        // POST: VenueBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookEventId,VenueId,VenueName,ClassId,ClassName,GradeId,GradeName,RefNum,DateBookinFor,StartTime,EndTime,BookingReason")] VenueBooking venueBooking)
        {
            if (ModelState.IsValid)
            {
                var getClassName = db.Classes.Find(venueBooking.ClassId);

                var studentsToEmail = db.Students.Where(x => x.ClassName == getClassName.ClassName).ToList();
                db.VenueBookings.Add(venueBooking);
                db.SaveChanges();

                foreach (var s in studentsToEmail)
                {
                    var message =
                        $@"Dear, {s.StudentName}

Please note {venueBooking.VenueName} has been booked for the {venueBooking.DateBookinFor}
during {venueBooking.StartTime} and {venueBooking.EndTime}.

This is an automated message genarated by DGS.
Thank you
";


                    SendEmailReminders(s.StudentEmail, message, s.ClassName);
                }



                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", venueBooking.ClassId);
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", venueBooking.GradeId);
            ViewBag.VenueId = new SelectList(db.Venues, "venueId", "venueName", venueBooking.VenueId);
            return View(venueBooking);
        }

        public void SendEmailReminders(string email, string message, string className)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dgstest2020@gmail.com", "Dgs2020!"),
                EnableSsl = true,
            };

            var subject = className + " | Notification";
            smtpClient.Send(email, email, "Booking", message);
        }


        // GET: VenueBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = db.VenueBookings.Find(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", venueBooking.ClassId);
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", venueBooking.GradeId);
            ViewBag.VenueId = new SelectList(db.Venues, "venueId", "venueName", venueBooking.VenueId);
            return View(venueBooking);
        }

        // POST: VenueBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookEventId,VenueId,VenueName,ClassId,ClassName,GradeId,GradeName,RefNum,DateBookinFor,StartTime,EndTime,BookingReason")] VenueBooking venueBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", venueBooking.ClassId);
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", venueBooking.GradeId);
            ViewBag.VenueId = new SelectList(db.Venues, "venueId", "venueName", venueBooking.VenueId);
            return View(venueBooking);
        }

        // GET: VenueBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueBooking venueBooking = db.VenueBookings.Find(id);
            if (venueBooking == null)
            {
                return HttpNotFound();
            }
            return View(venueBooking);
        }

        // POST: VenueBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueBooking venueBooking = db.VenueBookings.Find(id);
            db.VenueBookings.Remove(venueBooking);
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
