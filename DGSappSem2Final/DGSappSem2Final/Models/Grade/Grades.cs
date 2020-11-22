using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Grade
{
    public class Grades
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }

        [Required(ErrorMessage = "Enter Grade ")]
        [Display(Name = "Grade Name")]
        [CustomGradeNameValidator]
        public string GradeName { get; set; }

        [Display(Name = "Max No. Of Students In Grade")]
        [Required]
        [Range(1, 500)]
        public int MaxNoOfStudentsInGrade { get; set; }

        [Display(Name = "Max No. Of Classes")]
        [Required]
        [Range(1, 7)]
        public int MaxNoOfClasses { get; set; }

        [Display(Name = "Max No. Of Students Per Class")]
        [Range(1, 50)]
        public int MaxNoOfStudentsPerClass { get; set; }

    }
}