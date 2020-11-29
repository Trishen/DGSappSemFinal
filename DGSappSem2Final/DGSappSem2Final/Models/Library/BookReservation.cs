using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Library
{
    public class BookReservation
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public int? BookId { get; set; }
        public string BookName { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student.Student Student { get; set; }

        public int? StudentId { get; set; }
        public string StudentName { get; set; }

        //public int BookId { get; set; }
        //public DateTime ReservationDate { get; set; }
        //[DisplayName("Student Email")]

        //public string StudentEmail { get; set; }
        [DisplayName("Collection Date"), DataType(DataType.Date)]
        public DateTime CollectionDate { get; set; }

        [DisplayName("Return Date"), DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public string Status { get; set; }
        [Display(Name = "Message")]
        public string SMSMessage { get; set; }

        public IEnumerable<string> BookNameCollection { get; set; }
        public IEnumerable<string> StudentNameCollection { get; set; }

    }
}