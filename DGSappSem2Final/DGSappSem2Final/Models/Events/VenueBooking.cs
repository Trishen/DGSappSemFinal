using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Events
{
    public class VenueBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookEventId { get; set; }

        [ForeignKey("VenueId")]
        public virtual Venue Venue { get; set; }

        public int? VenueId { get; set; }

        [Display(Name = "Venue Name")]

        public string VenueName { get; set; }

        [ForeignKey("ClassId")]
        public virtual Classes.Classes Class { get; set; }

        public int? ClassId { get; set; }

        [Display(Name = "Class Name")]
        public string ClassName { get; set; }


        [ForeignKey("GradeId")]
        public virtual Grades Grade { get; set; }

        public int? GradeId { get; set; }

        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }



        //public Guid RefNum { get; internal set; } = Guid.NewGuid();

        [Display(Name = "Date Booking For"), DataType(DataType.Date), Required]
        [CustomDateValidator]
        public DateTime DateBookinFor { get; set; } = DateTime.Now.AddDays(1);


        [Display(Name = "Start Time"), DataType(DataType.Time), Required]
        //[CustomDateValidator]
        public DateTime StartTime { get; set; }


        [Display(Name = "End Time"), DataType(DataType.Time), Required]
        // [CustomDateValidator]
        public DateTime EndTime { get; set; }


        [Display(Name = "Reason for Booking")]
        [Required]
        public string BookingReason { get; set; }


    }
}