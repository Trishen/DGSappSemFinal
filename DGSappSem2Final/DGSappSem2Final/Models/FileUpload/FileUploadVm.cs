using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.FileUpload
{
    //public class FileUploadVm
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please select file.")]
    //    //public HttpPostedFileBase FileUpload { get; set; }

    //}
    public class FileUpload
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public List<FileUpload> FileList { get; set; }
    }



}