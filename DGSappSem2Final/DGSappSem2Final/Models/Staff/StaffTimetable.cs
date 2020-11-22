using DGSappSem2Final.Models.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Staff
{
    public class StaffTimetable
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StaffTimetableId { get; set; }

        public string Registration { get; set; } = "FREE";
        public ClassSessions Monday { get; set; } = new ClassSessions();
        public ClassSessions Tuesday { get; set; } = new ClassSessions();
        public ClassSessions Wednesday { get; set; } = new ClassSessions();
        public ClassSessions Thursday { get; set; } = new ClassSessions();
        public ClassSessions Friday { get; set; } = new ClassSessions();

        public bool HasAssignedClasses { get; set; }

        //Staff
        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }

        public int? StaffId { get; set; }
        public string StaffName { get; set; }

        [Display(Name = "Assigned Teacher")]
        public string AssignedTeacher { get; set; }

        public IEnumerable<string> TeacherNameCollection { get; set; }
    }
}