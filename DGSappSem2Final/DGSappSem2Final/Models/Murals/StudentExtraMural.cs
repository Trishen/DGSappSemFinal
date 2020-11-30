using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Murals
{
    public class StudentExtraMural
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StudentMuralId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student.Student Student { get; set; }

        public int? StudentId { get; set; }

        [ForeignKey("MuralAgeGroupId")]
        public virtual ExtraMuralAgeGroups ExtraMuralAgeGroup { get; set; }

        public int? MuralAgeGroupId { get; set; }

        [ForeignKey("MuralId")]
        public virtual ExtraMural ExtraMural { get; set; }

        public int? MuralId { get; set; }
    }
}