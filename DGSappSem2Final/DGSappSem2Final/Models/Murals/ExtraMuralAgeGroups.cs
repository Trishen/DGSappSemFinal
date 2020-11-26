using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Murals
{
    public class ExtraMuralAgeGroups
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MuralAgeGroupId { get; set; }

        public string MuralName { get; set; }
        [ForeignKey("ExtraMuralId")]
        public virtual ExtraMural ExtraMural { get; set; }

        public int? ExtraMuralId { get; set; }

        [Display(Name = "Extra Mural Name")]
        [Required]
        public string ExtraMuralName { get; set; }

        [Required]
        [DisplayName("Age Group Name")]
        public Enums.AgeGroups AgeGroupName { get; set; }

        [DisplayName("No. Assigned Students")]
        public int NoAssignedStudents { get; set; }

        [ForeignKey("StaffId")]
        public virtual Staff.Staff Staff { get; set; }

        public int? StaffId { get; set; }

        [Display(Name = "Assigned Teacher")]
        public string AssignedTeacher { get; set; }

        public IEnumerable<string> TeacherNameCollection { get; set; }
    }
}