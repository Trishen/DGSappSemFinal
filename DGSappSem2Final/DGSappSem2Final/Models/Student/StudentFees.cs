using DGSappSem2Final.Models.Grade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Student
{
    public class StudentFees
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StudentFeeId { get; set; }

        //Grade
        [ForeignKey("FeeId")]
        public virtual Fee Fee { get; set; }

        public int? FeeId { get; set; }

        [Display(Name = "Grade Fee")]
        public double GradeFee { get; set; }

        public double FeePaid { get; set; }

        [Display(Name = "Outstanding Balance")]
        public double FeeBalance { get; set; }


        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public int? StudentId { get; set; }

        [Display(Name = "Student Name")]
        [Required]
        public string StudentName { get; set; }


        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }
    }
}