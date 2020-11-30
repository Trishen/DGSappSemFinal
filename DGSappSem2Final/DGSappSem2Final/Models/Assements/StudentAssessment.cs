using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Subject;
using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Assements
{
    public class StudentAssessment
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StudentAssessmentID { get; set; }



        //Grade
        [ForeignKey("StaffAssessmentId")]
        public virtual StaffAssessment StaffAssessment { get; set; }

        public int? StaffAssessmentId { get; set; }

        //[Display(Name = "Assessment Name")]
        //public string StaffAssessmentName { get; set; }

        //[Display(Name = "File Name")]
        //public string FileName { get; set; }
        //public string FileType { get; set; }
        //public byte[] File { get; set; }

        //public string DownloadPath { get; set; }
    }
}