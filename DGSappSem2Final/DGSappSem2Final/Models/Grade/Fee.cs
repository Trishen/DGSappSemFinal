using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Grade
{
    public class Fee
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int FeeId { get; set; }

        //Grade
        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }

        [Display(Name = "Grade Fee")]
        [Required]
        public double GradeFee { get; set; }

        public IEnumerable<string> GradeNameCollection { get; set; }

    }
}