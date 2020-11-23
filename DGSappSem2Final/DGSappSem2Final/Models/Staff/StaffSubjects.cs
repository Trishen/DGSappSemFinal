using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Subject;
using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Staff
{
    public class StaffSubjects
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StaffSubjectId { get; set; }

        //GradeSubject
        [ForeignKey("SubjectId")]
        public virtual GradeSubjects GradeSubject { get; set; }

        public int? GradeSubjectId { get; set; }

        public string GradeName { get; set; }


        //Subject
        [ForeignKey("SubjectId")]
        public virtual Subjects Subject { get; set; }

        public int? SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        //Staff
        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }

        public int? StaffId { get; set; }

        [Display(Name = "Assigned Teacher")]
        public string AssignedTeacher { get; set; }

        public IEnumerable<string> TeacherNameCollection { get; set; }
        public IEnumerable<string> SubjectNameCollection { get; set; }
    }
}