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
                        AssignedTeacher = displayName,
                        TimeTableLayout = GetTimeTableLayout(new List<StaffSubjects>())
                    });
                    db.SaveChanges();
                }
            }

            //TimeTableReset();

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

            if (!staffTimetable.TimeTableAssigned)
            {
                var reg = db.Classes.ToList();
                var assignedReg = reg.Where(x => x.AssignedTeacher == staffTimetable.AssignedTeacher).ToList();

                var staffSubs = db.StaffSubjects.ToList();
                var assignedSubs = staffSubs.Where(x => x.AssignedTeacher == staffTimetable.AssignedTeacher).ToList();

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

                if (assignedSubs.Count > 0)
                {
                    staffTimetable.TimeTableLayout = GetTimeTableLayout(assignedSubs);

                    staffTimetable.Monday = staffTimetable.TimeTableLayout[0];
                    staffTimetable.Tuesday = staffTimetable.TimeTableLayout[1];
                    staffTimetable.Wednesday = staffTimetable.TimeTableLayout[2];
                    staffTimetable.Thursday = staffTimetable.TimeTableLayout[3];
                    staffTimetable.Friday = staffTimetable.TimeTableLayout[4];
                }

                if (staffTimetable == null)
                {
                    return HttpNotFound();
                }

                staffTimetable.TimeTableAssigned = true;
                db.SaveChanges();
            }

            return View(staffTimetable);
        }

        private ClassSessions[] GetTimeTableLayout(List<StaffSubjects> assignedStaffSubs)
        {
            var template = GetTimeTableTemplate();

            foreach (var assignedSub in assignedStaffSubs)
            {
                var gradeInfo = db.GradeSubjects
                    .Where(x => x.Grade.GradeName == assignedSub.GradeName
                    && x.Subject.SubjectName == assignedSub.SubjectName).FirstOrDefault();

                var lessionCount = gradeInfo.NoOfLessonsRequired;

                for (int i = 0; i < lessionCount; i++)
                {
                    template = AddLesson(template, gradeInfo.Grade.GradeName, gradeInfo.Subject.SubjectName);
                }
            }
            return template;
        }

        private ClassSessions[] AddLesson(ClassSessions[] classSessions, string gradeName, string subjectName)
        {
            var rDay = 0;
            var rSession = 0;
            var block = false;

            do
            {
                rDay = GetRandomDay();
                rSession = GetRandomSession();
                block = SessionFree(classSessions, rDay, rSession);

            } while (block == false);


            classSessions = SetSession(classSessions, rDay, rSession, gradeName, subjectName);



            return classSessions;
        }

        private ClassSessions[] SetSession(ClassSessions[] classSessions, int day, int session, string grade, string subject)
        {
            var description = grade + "\n" + subject;

            day = day - 1;

            if (session == 1)
            {
                classSessions[day].ClassSession1 = description;
            }

            if (session == 2)
            {
                classSessions[day].ClassSession2 = description;
            }
            if (session == 3)
            {
                classSessions[day].ClassSession3 = description;
            }
            if (session == 4)
            {
                classSessions[day].ClassSession4 = description;
            }
            if (session == 5)
            {
                classSessions[day].ClassSession5 = description;
            }
            if (session == 6)
            {
                classSessions[day].ClassSession6 = description;
            }

            return classSessions;
        }


        private bool SessionFree(ClassSessions[] classSessions, int day, int session)
        {
            day = day - 1;
            if (session == 1)
            {
                return classSessions[day].ClassSession1 == "FREE" || classSessions[day].ClassSession1 == null;
            }

            if (session == 2)
            {
                return classSessions[day].ClassSession2 == "FREE" || classSessions[day].ClassSession2 == null;
            }
            if (session == 3)
            {
                return classSessions[day].ClassSession3 == "FREE" || classSessions[day].ClassSession3 == null;
            }
            if (session == 4)
            {
                return classSessions[day].ClassSession4 == "FREE" || classSessions[day].ClassSession4 == null;
            }
            if (session == 5)
            {
                return classSessions[day].ClassSession5 == "FREE" || classSessions[day].ClassSession5 == null;
            }
            if (session == 6)
            {
                return classSessions[day].ClassSession6 == "FREE" || classSessions[day].ClassSession6 == null;
            }

            return false;
        }

        //To allow for adding specified frees
        private ClassSessions[] GetTimeTableTemplate()
        {
            var classSessions = new ClassSessions[5];
            classSessions[0] = new ClassSessions();
            classSessions[1] = new ClassSessions();
            classSessions[2] = new ClassSessions();
            classSessions[3] = new ClassSessions();
            classSessions[4] = new ClassSessions();

            return classSessions;
        }

        private int GetRandomDay()
        {
            return GetRandomValue(6);
        }

        private int GetRandomSession()
        {
            return GetRandomValue(7);
        }

        private int GetRandomValue(int max)
        {
            var random = new Random();
            return random.Next(1, max);
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

        private void TimeTableReset()
        {
            foreach (var t in db.StaffTimetables.ToList())
            {
                t.TimeTableAssigned = false;
                t.Monday.ClassSession1 = "FREE";
                t.Monday.ClassSession2 = "FREE";
                t.Monday.ClassSession3 = "FREE";
                t.Monday.ClassSession4 = "FREE";
                t.Monday.ClassSession5 = "FREE";
                t.Monday.ClassSession6 = "FREE";

                t.Tuesday.ClassSession1 = "FREE";
                t.Tuesday.ClassSession2 = "FREE";
                t.Tuesday.ClassSession3 = "FREE";
                t.Tuesday.ClassSession4 = "FREE";
                t.Tuesday.ClassSession5 = "FREE";
                t.Tuesday.ClassSession6 = "FREE";

                t.Wednesday.ClassSession1 = "FREE";
                t.Wednesday.ClassSession2 = "FREE";
                t.Wednesday.ClassSession3 = "FREE";
                t.Wednesday.ClassSession4 = "FREE";
                t.Wednesday.ClassSession5 = "FREE";
                t.Wednesday.ClassSession6 = "FREE";

                t.Thursday.ClassSession1 = "FREE";
                t.Thursday.ClassSession2 = "FREE";
                t.Thursday.ClassSession3 = "FREE";
                t.Thursday.ClassSession4 = "FREE";
                t.Thursday.ClassSession5 = "FREE";
                t.Thursday.ClassSession6 = "FREE";

                t.Friday.ClassSession1 = "FREE";
                t.Friday.ClassSession2 = "FREE";
                t.Friday.ClassSession3 = "FREE";
                t.Friday.ClassSession4 = "FREE";
                t.Friday.ClassSession5 = "FREE";
                t.Friday.ClassSession6 = "FREE";

                db.SaveChanges();
            }
        }


    }
}
