using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Subject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Assements
{
    public class AssessmentOLD
    {
        ////Property key
        //[Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        //public int AssessmentID { get; set; }


        //[Display(Name = "Assessment Name")]
        //public int AssessmentName { get; set; }

        ////textbox
        //[ForeignKey("GradeId")]
        //public virtual Grades Grade { get; set; }

        //public int? GradeId { get; set; }

        //[Display(Name = "Grade Name")]
        //[Required]
        //public string GradeName { get; set; }

        //[Display(Name = "Max No. Of Classes")]
        ////Subject
        //[ForeignKey("SubjectId")]
        //public virtual Subjects Subject { get; set; }

        //public int? SubjectId { get; set; }

        //[Display(Name = "Subject Name")]
        //public string SubjectName { get; set; }

        ////Make string
        //[Display(Name = "Assessment Due Date "), DataType(DataType.Date)]
        //[Required]
        //public DateTime AssessmentDate { get; set; } = DateTime.Now;

        //[Required(ErrorMessage = "Select Term ")]
        //public string Term { get; set; }


        //public bool HasUploadFile { get; set; }

        //public string FileName { get; set; }
        //[Required(ErrorMessage = "File Path")]

        //[DisplayName("Upload File")]
        //public string ImagePath { get; set; }
        //[Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        //public HttpPostedFile ImageFile { get; set; }
        //public string FileType { get; set; }
        //public byte[] File { get; set; }


        public IEnumerable<string> GradeNameCollection { get; set; }
        public IEnumerable<string> TermCollection { get; set; }
        public IEnumerable<string> SubjectNameCollection { get; set; }
    }
}