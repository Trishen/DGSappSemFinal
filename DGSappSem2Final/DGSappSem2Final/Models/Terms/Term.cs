using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Terms
{
    public class Term
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int TermId { get; set; }

        [Required(ErrorMessage = "Enter Term ")]
        [Display(Name = "Term")]
        [CustomTermNameValidator]
        public string TermName { get; set; }
    }
}