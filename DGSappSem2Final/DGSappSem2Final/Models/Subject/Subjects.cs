using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Subject
{
    public class Subjects
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        [Required(ErrorMessage = "Enter Subject Name")]
        [CustomSubjectNameValidator]
        public string SubjectName { get; set; }
    }
}