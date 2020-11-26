using DGSappSem2Final.Models.Grade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Assements
{
    public class Exam
    {
        //Property key
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int AssessmentID { get; set; }

        //textbox
        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }

        [Display(Name = "Max No. Of Classes")]

        //start and end time
        //[DataType(DataType.Time)]
        //[Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        //[Display(Name = "Start Time:")]
        //public DateTime StartTime { get; set; }

        //[DataType(DataType.Time)]
        //[Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        //[Display(Name = "End Time:")]
        //public DateTime EndTime { get; set; }


 
        //Make string
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime ExamDate { get; set; }


        [Required(ErrorMessage = "Enter Test Venue")]
        [Display(Name = "Assessment Venue")]
        public string ExamVenue { get; set; }


        [Required(ErrorMessage = "Select Term ")]
        public string Term { get; set; }

        //radiobutton (test,project,presentation,exam)
        [Required(ErrorMessage = "Enter Test Type")]
        [Display(Name = "Assessment Type ")]
        public string Type { get; set; }

        //collections
        //  public ICollection<SubjectReport> SubjectReports { get; set; }


    }
}