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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Class).Include(s => s.Staff);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var student = new Student
            {
                GradeNameCollection = db.Grades.Select(x => x.GradeName).ToList(),
            };

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title");
            return View(new Student());
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StID,StudentName,StudentSurname,StudentGender,StudentAddress,StudentTown,StudentContact,StudentGrade,StudentEmail,StudentBirthCertURL,Title,ParentName,ParentSurName,ParentContact,ParentEmail,StudentAllowReg,StaffId,AssignedTeacher,ClassId,ClassName")] Student student)
        {
            student.AssignedTeacher = db.Classes.Where(x => x.GradeName == student.StudentGrade).Select(x => x.AssignedTeacher).FirstOrDefault();
            student.ClassName = db.Classes.Where(x => x.GradeName == student.StudentGrade).Select(x => x.ClassName).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", student.ClassId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", student.StaffId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", student.ClassId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", student.StaffId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StID,StudentName,StudentSurname,StudentGender,StudentAddress,StudentTown,StudentContact,StudentGrade,StudentEmail,StudentBirthCertURL,Title,ParentName,ParentSurName,ParentContact,ParentEmail,StudentAllowReg,StaffId,AssignedTeacher,ClassId,ClassName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", student.ClassId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", student.StaffId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
