using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DGSappSem2Final.Models.Staff;

namespace DGSappSem2Final.Models.Classes
{
    public class Classes
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        //Grade
        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }

        [Display(Name = "Max No. Of Classes")]

        public int MaxNoOfClasses { get; set; }

        [Display(Name = "Max No. Of Students Per Class")]
        public int MaxNoOfStudentsPerClass { get; set; }


        [Display(Name = "No. Of Assigned Students")]
        public int NoOfAssignedStudents { get; set; }


        //Staff
        [ForeignKey("StaffId")]
        public virtual Staff.Staff Staff { get; set; }

        public int? StaffId { get; set; }

        [Display(Name = "Assigned Teacher")]
        [Required]
        public string AssignedTeacher { get; set; }


        public IEnumerable<string> GradeNameCollection { get; set; }
        public IEnumerable<string> TeacherNameCollection { get; set; }
        //public IEnumerable<string> AssignedTeacherCollection { get; set; }

    }
}