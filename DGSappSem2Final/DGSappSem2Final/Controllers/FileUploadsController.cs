using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Shapes;
using DGSappSem2Final.Models;
using DGSappSem2Final.Models.FileUpload;

namespace DGSappSem2Final.Controllers
{
    public class FileUploadsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: FileUploads
        //public ActionResult Index()
        //{
        //    return View(db.FileUploads.ToList());
        //}

        //// GET: FileUploads/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FileUpload fileUpload = db.FileUploads.Find(id);
        //    if (fileUpload == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fileUpload);
        //}

        //// GET: FileUploads/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: FileUploads/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FileId,FileName,FileUrl")] FileUpload fileUpload)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FileUploads.Add(fileUpload);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(fileUpload);
        //}

        //// GET: FileUploads/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FileUpload fileUpload = db.FileUploads.Find(id);
        //    if (fileUpload == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fileUpload);
        //}

        //// POST: FileUploads/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FileId,FileName,FileUrl")] FileUpload fileUpload)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(fileUpload).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(fileUpload);
        //}

        //// GET: FileUploads/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FileUpload fileUpload = db.FileUploads.Find(id);
        //    if (fileUpload == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fileUpload);
        //}

        //// POST: FileUploads/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    FileUpload fileUpload = db.FileUploads.Find(id);
        //    db.FileUploads.Remove(fileUpload);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}













        //List<FileUpload> _List = new List<FileUpload>();

        ////public bool UploadEnabled { get; set; }

        //private ApplicationDbContext db = new ApplicationDbContext();
        //// GET: FileUploadVm
        //public ActionResult Index()
        //{
        //    List<FileUploadModel> fileUp = db.fileUploadModel.ToList();
        //    return View(fileUp);
        //}
        //public ActionResult FileUploadProcess()
        //{
        //    var model = new FileUploadVm();
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult FileUploadProcess(FileUploadVm model, FileUploadBusiness fileUploadBusiness)
        //{
        //    fileUploadBusiness.UploadFile(model);
        //    return RedirectToAction("Index");
        //}
        //public FileContentResult FileDownload(int? id, FileUploadBusiness fileUploadBusiness)
        //{
        //    var file = fileUploadBusiness.SearchFile(id);
        //    return File(fileUploadBusiness.fileData(file), "text", fileUploadBusiness.fileName(file));
        //}

        //public ActionResult Upload()
        //{
        //    return View("Index");
        //}

        //public bool SetUploadEnabled(FileUploadVm model)
        //{
        //    return model.FileName != string.Empty;
        //}
        //string conString = "Data Source=.;Initial Catalog =DemoTest; integrated security=true;";

        //string conString = "Data Source=.;Initial Catalog =DemoTest; integrated security=true;";

        public int count = 1;
        System.Guid guid = System.Guid.NewGuid();

        // GET: Files  
        public ActionResult Index(FileUpload model)
        {
            var dtFiles = GetFileDetails();

            List<FileUpload> list = new List<FileUpload>();
            foreach (var item in dtFiles)
            {
                list.Add(new FileUpload
                {
                    //FileId = item.FileId,
                    FileId = (count++).ToString(),
                    FileName = item.FileName,
                    FileUrl = item.FileUrl
                });
            }
            model.FileList = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files)
        {
            FileUpload model = new FileUpload
            {
                FileList = new List<FileUpload>()
            };
            FileUpload file;
            var dtFiles = GetFileDetails();
            foreach (var item in dtFiles)
            {
                file = new FileUpload
                {
                    FileId = item.FileId,
                    FileName = item.FileName,
                    FileUrl = item.FileUrl
                };
                model.FileList.Add(file);
            }

            if (files != null)
            {
                //var Extension = Path.GetExtension(files.FileName);
                //var fileName = "my-file-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), files.FileName);
                model.FileUrl = Url.Content(System.IO.Path.Combine("~/UploadedFiles/", files.FileName));
                model.FileName = files.FileName;

                if (SaveFile(model))
                {
                    files.SaveAs(path);
                    TempData["AlertMessage"] = "Uploaded Successfully !!";
                    return RedirectToAction("Index", "FileUpload");
                }
                else
                {
                    ModelState.AddModelError("", "Error In Add File. Please Try Again !!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                return View(model);
            }
            return RedirectToAction("Index", "FileUpload");
        }

        private List<FileUpload> GetFileDetails()
        {
            //DataTable dtData = new DataTable();
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand command = new SqlCommand("Select * From tblFileDetails", con);
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(dtData);
            //con.Close();
            //return dtData;
            var response = new List<FileUpload>();

            var path = Server.MapPath("~/UploadedFiles");

            var test = Directory.GetFiles(path);
            foreach (var entry in test)
            {
                var file = new FileUpload
                {

                    FileId = entry,
                    FileName = System.IO.Path.GetFileName(entry),
                    FileUrl = entry

                };
                guid.ToString();
                response.Add(file);
            }

            return response;
        }

        private bool SaveFile(FileUpload model)
        {
            //string strQry = "INSERT INTO tblFileDetails (FileName,FileUrl) VALUES('" +
            //    model.FileName + "','" + model.FileUrl + "')";
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand command = new SqlCommand(strQry, con);
            //int numResult = command.ExecuteNonQuery();
            //con.Close();
            if (model != null)

            {
                model.FileList.Add(model);
                guid.ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
        //public string filePath = @"E:\work\campus work\3rd year 2020\Project work\sem 2\project code\DGSrepo1\DGSappSem2\DGSappSem2\DGSappSem2\Files\DownloadFiles";
        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);


        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }


        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FileUpload assessment = await db.FileUpload.FindAsync(id);
        //    if (assessment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(assessment);
        //}

        //// POST: Assessments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    FileUpload assessment = await db.FileUpload.FindAsync(id);
        //    db.Assessments.Remove(assessment);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}






    }
}
