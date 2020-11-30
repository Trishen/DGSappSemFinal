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
    public class StaffAssessment
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int AssessmentID { get; set; }

        [CustomAssessmentNameValidator]
        [Display(Name = "Assessment Name")]
        public string AssessmentName { get; set; }

        //Grade
        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
       
        public string GradeName { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }

        public string DownloadPath { get; set; }
    }
}