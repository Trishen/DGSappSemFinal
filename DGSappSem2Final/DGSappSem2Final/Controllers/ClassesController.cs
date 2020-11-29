using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Classes;

namespace DGSappSem2Final.Controllers
{
    public class ClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classes
        public ActionResult Index()
        {
            var classes = db.Classes.Include(c => c.Grade).Include(c => c.Staff);
            var students = db.Students.ToList() ;

            foreach(var c in db.Classes.ToList())
            {
                c.NoOfAssignedStudents = students.Where(x=> x.ClassName == c.ClassName).Count();
            }
            
            return View(classes.ToList());
        } 
        
        public ActionResult ClassList(int id)
        {
            var className = db.Classes.Find(id).ClassName;
            var students = db.Students.Where(c => c.ClassName == className).ToList();

            return View(students);
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classes classes = db.Classes.Find(id);
            if (classes == null)
            {
                return HttpNotFound();
            }
            return View(classes);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            Dictionary<int, string> teacherCollection = GetTeacherNameComboCollection();
            Dictionary<int, string> gradeCollection = GetGradeNameComboCollection();
            var classes = new Classes
            {
                GradeNameCollection = gradeCollection.Values.ToList(),
                TeacherNameCollection = teacherCollection.Values.ToList()
            };

           
            return View(classes);
        }

        private Dictionary<int, string> GetTeacherNameComboCollection()
        {
            var teacherCollection = new Dictionary<int, string>();

            var collection = db.Staffs.ToList();

            foreach (var entry in collection)
            {
                var displayName = $"{entry.Title}. {entry.Name} {entry.Surname}";
                var hasAssignedClass = db.Classes.Any(x => x.AssignedTeacher.Equals(displayName));

                if (entry.StaffPositionName != "Principle" && entry.StaffPositionName != "Vice Principle" && !hasAssignedClass)
                {
                    teacherCollection.Add(entry.StaffId, displayName);
                }
            }

            return teacherCollection;
        }

        private Dictionary<int, string> GetGradeNameComboCollection()
        {
            var gradeCollection = new Dictionary<int, string>();

            var collection = db.Grades.ToList();

            foreach (var entry in collection)
            {
                var classesCount = db.Classes.ToList().Where(x => x.GradeName.Equals(entry.GradeName)).Count();

                if (!classesCount.Equals(entry.MaxNoOfClasses))
                {
                    gradeCollection.Add(entry.GradeId, entry.GradeName);
                }
            }

            return gradeCollection;
        }


        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassId,ClassName,Grade,GradeId,GradeName,MaxNoOfClasses,Staff,StaffId,AssignedTeacher,MaxNoOfStudentsPerClass")] Classes classes)
        {
            Dictionary<int, string> teacherCollection = GetTeacherNameComboCollection();
            Dictionary<int, string> gradeCollection = GetGradeNameComboCollection();

            classes.TeacherNameCollection = teacherCollection.Values.ToList();
            classes.GradeNameCollection = gradeCollection.Values.ToList();

            classes.GradeId = FindFirstKeyByValue(gradeCollection, classes.GradeName);
            classes.MaxNoOfClasses = db.Grades.Find(classes.GradeId).MaxNoOfClasses;
            classes.MaxNoOfStudentsPerClass = db.Grades.Find(classes.GradeId).MaxNoOfStudentsPerClass;
            classes.StaffId = FindFirstKeyByValue(teacherCollection, classes.AssignedTeacher);

            if (ModelState.IsValid)
            {
                db.Classes.Add(classes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", classes.GradeId);
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", classes.StaffId);
            return View(classes);
        }

        public int FindFirstKeyByValue(Dictionary<int, string> dict, string value)
        {
            foreach (KeyValuePair<int, string> pair in dict)
            {
                if (EqualityComparer<string>.Default.Equals(pair.Value, value))
                {
                    return pair.Key;
                }
            }
            return default;
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            Dictionary<int, string> teacherCollection = GetTeacherNameComboCollection();
            Dictionary<int, string> gradeCollection = GetGradeNameComboCollection();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classes classes = db.Classes.Find(id);

            classes.GradeNameCollection = gradeCollection.Values.ToList();
            classes.TeacherNameCollection = teacherCollection.Values.ToList();

            classes.GradeId = FindFirstKeyByValue(gradeCollection, classes.GradeName);
            //classes.MaxNoOfClasses = db.Grades.Find(classes.GradeId).MaxNoOfClasses;
            classes.StaffId = FindFirstKeyByValue(teacherCollection, classes.AssignedTeacher);

            if (classes == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", classes.GradeId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", classes.StaffId);
            return View(classes);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassId,ClassName,Grade,GradeId,GradeName,MaxNoOfClasses,Staff,StaffId,AssignedTeacher,MaxNoOfStudentsPerClass")] Classes classes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeName", classes.GradeId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "Title", classes.StaffId);
            return View(classes);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classes classes = db.Classes.Find(id);
            if (classes == null)
            {
                return HttpNotFound();
            }
            return View(classes);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classes classes = db.Classes.Find(id);
            db.Classes.Remove(classes);
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
