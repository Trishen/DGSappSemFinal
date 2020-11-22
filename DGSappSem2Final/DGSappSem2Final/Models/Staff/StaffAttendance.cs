using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Staff
{
    public class StaffAttendance
    {
        [Key]
        public int StaffAttendanceId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string StaffAttName { get; set; }

        [Required]
        [Display(Name = "Clock In / Clock Out")]
        public string Staffrecord { get; set; }

        [Display(Name = "Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy h:mm:ss}")]
        public DateTime GetDate { get; set; } = new DateTime(DateTime.Now.Year, month: DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        public List<string> StaffCollection { get; set; }
    }
}