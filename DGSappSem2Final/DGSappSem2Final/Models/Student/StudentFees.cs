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

        [Display(Name = "Outstanding Balance")]
        public double FeeBalance { get; set; }
    }
}