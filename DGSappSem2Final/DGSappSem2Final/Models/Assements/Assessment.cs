using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Subject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Assements
{
    public class Assessment
    {
        //Property key
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int AssessmentID { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFile ImageFile { get; set; }
    }
}