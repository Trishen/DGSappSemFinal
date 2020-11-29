using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Subject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Assements
{
    public class Assessment
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int AssessmentID { get; set; }
        public int AssessmentName { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }

        public string DownloadPath { get; set; }
    }
}