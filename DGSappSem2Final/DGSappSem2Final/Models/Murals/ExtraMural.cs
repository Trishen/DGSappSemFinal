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
    public class ExtraMural
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MuralId { get; set; }

        [Required]
        [DisplayName("Mural Name")]
        [CustomExtraMuralNameValidator]
        public string MuralName { get; set; }
    }
}