﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Library;
using DGSappSem2Final.Models.Student;

namespace DGSappSem2Final.Controllers
{
    public class BookReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookReservations
        public ActionResult Index()
        {
            return View(db.BookReservations.ToList());
        }

        // GET: BookReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReservation bookReservation = db.BookReservations.Find(id);
            if (bookReservation == null)
            {
                return HttpNotFound();
            }
            return View(bookReservation);
        }

        // GET: BookReservations/Create
        public ActionResult Create()
        {
            var books = db.Books.ToList();
            var students = db.Students.ToList();

            var bookrev = new BookReservation
            {
                BookNameCollection = GetBookNames(books),
                StudentNameCollection = GetStudentNames(students)
            };

            return View(bookrev);
        }

        private List<string> GetBookNames(List<Book> books)
        {
            var list = new List<string>();
            var bookres = db.BookReservations.ToList();


            foreach (var st in books)
            {
                if(!bookres.Any(x=> x.BookName == st.Title))
                {
                    list.Add(st.Title);
                }
            }

            return list;
        }

        private List<string> GetStudentNames(List<Student> students)
        {
            var list = new List<string>();

            foreach(var st in students)
            {
                var displayName = st.StudentName + " " + st.StudentSurname + " (" + st.ClassName + ")";

                list.Add(displayName);
            }

            return list;
        }



        // POST: BookReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,BookName,StudentName,CollectionDate,ReturnDate,Status")] BookReservation bookReservation)
        {
            bookReservation.CollectionDate = DateTime.Now.Date;
            bookReservation.ReturnDate = DateTime.Now.Date.AddDays(14);
            bookReservation.Status = Enums.BookStatus.Booked.ToString();

            if (ModelState.IsValid)
            {
                db.BookReservations.Add(bookReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookReservation);
        }

        // GET: BookReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReservation bookReservation = db.BookReservations.Find(id);
            if (bookReservation == null)
            {
                return HttpNotFound();
            }
            return View(bookReservation);
        }

        // POST: BookReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,BookId,StudentEmail,CollectionDate,ReturnDate,Status")] BookReservation bookReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookReservation);
        }

        // GET: BookReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReservation bookReservation = db.BookReservations.Find(id);
            if (bookReservation == null)
            {
                return HttpNotFound();
            }
            return View(bookReservation);
        }

        // POST: BookReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookReservation bookReservation = db.BookReservations.Find(id);
            db.BookReservations.Remove(bookReservation);
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
