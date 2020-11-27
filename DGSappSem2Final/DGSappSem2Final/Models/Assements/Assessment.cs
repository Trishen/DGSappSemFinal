using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Subject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Assements
{
    public class Assessment
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

        //Subject
        [ForeignKey("SubjectId")]
        public virtual Subjects Subject { get; set; }

        public int? SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        //Make string
        [Display(Name = "Assessment Date "), DataType(DataType.Date)]
        [Required]
        public DateTime AssessmentDate { get; set; } = DateTime.Now;


        //[Required(ErrorMessage = "Enter Test Venue")]
        //[Display(Name = "Assessment Venue")]
        //public string AssessmentVenue { get; set; }


        [Required(ErrorMessage = "Select Term ")]
        public string Term { get; set; }


        public bool HasUploadFiles { get; set; }


        //radiobutton (test,project,presentation,exam)
        //[Required(ErrorMessage = "Enter Test Type")]
        //[Display(Name = "Assessment Type ")]
        //public string Type { get; set; }

        //collections
        //  public ICollection<SubjectReport> SubjectReports { get; set; }

        public IEnumerable<string> GradeNameCollection { get; set; }
        public IEnumerable<string> TermCollection { get; set; }
    }
}