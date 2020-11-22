using DGSappSem2Final.Models.Subject;
using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Grade
{
    public class GradeSubjects
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int GradeSubjectId { get; set; }
               
        //Grade
        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }

        //Subject
        [ForeignKey("SubjectId")]
        public virtual Subjects Subject { get; set; }

        public int? SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required]
        [Display(Name = "No. Of Lessons Required")]
        [Range(1,6)]
        public int NoOfLessonsRequired { get; set; }

    }
}