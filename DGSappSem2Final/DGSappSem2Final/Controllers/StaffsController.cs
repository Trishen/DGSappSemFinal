using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using QRCoder;

namespace DGSappSem2Final.Controllers
{
    public class StaffsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Staffs
        public ActionResult Index()
        {
            var bug = db.Staffs.Where(x=> x.StaffPositionId!=null).ToList();

            foreach(var t in bug)
            {
                t.StaffPositionId = null;
                db.SaveChanges();
            }



            var staffs = db.Staffs.Include(s => s.Grade);
            return View(staffs.ToList());
        }

        [HttpGet]
        public ActionResult Print()
        {
            return View();
        }

        [HttpGet]
        public ActionResult QRCode(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            string qr = staff.Name;
            ViewBag.Name = staff.Name;
            //ViewBag.Sub = staff.SubId.ToString();
            ViewBag.Con = staff.PhoneNo;
            QRCodeGenerator ObjQr = new QRCodeGenerator();

            QRCodeData qrCodeData = ObjQr.CreateQrCode(qr, QRCodeGenerator.ECCLevel.Q);

            Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);

            using (MemoryStream ms = new MemoryStream())

            {

                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] byteImage = ms.ToArray();

                ViewBag.Url = "data:image/png;base64," + Convert.ToBase64String(byteImage);

            }

            return View();

        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            var staffs = db.Staffs.ToList();
            var positions = db.StaffPositions.ToList();
            var positionsCollection = new Dictionary<int, string>();

            foreach (var pos in positions)
            {
                var limitReached = staffs.Count(x => x.StaffPositionName.Equals(pos.StaffPositionName)) == pos.Limit;

                if (!pos.LimitedPosition)
                {
                    positionsCollection.Add(pos.StaffPositionId, pos.StaffPositionName);
                }

                if (pos.LimitedPosition && !limitReached)
                {
                    positionsCollection.Add(pos.StaffPositionId, pos.StaffPositionName);
                }
            }

            var staff = new Staff
            {
                StaffPositionCollection = positionsCollection.Values.ToList()
            };

            return View(staff);
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,Title,Name,Surname,Gender,DateOfBirth,Email,PhoneNo,Address,PostalCode,StaffPositionId,StaffPositionName")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffPositionId = new SelectList(db.StaffPositions, "StaffPositionId", "StaffPositionName", staff.StaffPositionId);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            var staffs = db.Staffs.ToList();
            var positions = db.StaffPositions.ToList();
            var positionsCollection = new Dictionary<int, string>();

            foreach (var pos in positions)
            {
                var limitReached = staffs.Count(x => x.StaffPositionName.Equals(pos.StaffPositionName)) == pos.Limit;

                if (!pos.LimitedPosition)
                {
                    positionsCollection.Add(pos.StaffPositionId, pos.StaffPositionName);
                }

                if (pos.LimitedPosition && !limitReached)
                {
                    positionsCollection.Add(pos.StaffPositionId, pos.StaffPositionName);
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }

            staff.StaffPositionCollection = positionsCollection.Values.ToList();
          
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,Title,Name,Surname,Gender,DateOfBirth,Email,PhoneNo,Address,PostalCode,StaffPositionId,StaffPositionName")] Staff staff)
        {

            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.StaffPositionId = new SelectList(db.StaffPositions, "StaffPositionId", "StaffPositionName", staff.StaffPositionId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
