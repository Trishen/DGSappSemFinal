using DGSappSem2Final.Models.Grade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Subject
{
    public class SubjectAssignment
    {

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int IDMap { get; set; }

        [Display(Name = "Subject Id")]
        [ForeignKey("SubjectId")]
        public virtual Subjects Subject { get; set; }

        public int? SubjectId { get; set; }

        [Display(Name = "Grade Id")]
        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }

        [Display(Name = "Grade Name")]
        public List<string> SupportedGradesCollection { get; set; }

        [Display(Name = "Assigned Teacher Id")]
        [ForeignKey("StaffId")]
        public virtual Staff.Staff Staff { get; set; }

        public int? StaffId { get; set; }

        [Display(Name = "Assigned Teacher")]
        public string AssignedTeacher { get; set; }

        [Display(Name = "Class Id")]
        [ForeignKey("Class")]
        public virtual Classes.Classes Class { get; set; }

        public int? ClassId { get; set; }

        [Display(Name = "Class Name")]
        public string ClasseName { get; set; }

        public IEnumerable<string> GradeNameCollection { get; set; }
        public IEnumerable<string> TeacherNameCollection { get; set; }


    }
}