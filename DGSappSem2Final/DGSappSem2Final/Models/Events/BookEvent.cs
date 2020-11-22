using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Events
{
    public class BookEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookEventId { get; set; }


        [Display(Name = "Number Of Ocupants"), Required]
        [Range(1, 10000)]
        public int Quantiy { get; set; }
        //[Display(Name = "Number of participants")]
        //public int numParticipant { get; set; }
        public Guid RefNum { get; internal set; } = Guid.NewGuid();

        [Display(Name = "Date Booking For"), DataType(DataType.Date), Required]
        public DateTime DateBookinFor { get; set; }
        public bool CheckDate()
        {
            bool result = false;

            if (DateBookinFor == DateTime.Now || DateBookinFor < DateTime.Now.Date)
            {
                result = true;
            }
            return result;
        }

        //public int venueID { get; set; }
        //public virtual Venue Venue { get; set; }





        //ApplicationDbContext db = new ApplicationDbContext();

        //public int eventid;
        //// linq to get event price
        //public decimal? geteventprice()
        //{
        //    var prc = (from v in db.Events
        //               where eventid == v.eventid
        //               select v.bookingprice).firstordefault();
        //    return prc;
        //}

        //public int studentid;

        //// linq to get event price 
        //public string getstudentemail()
        //{
        //    var stuemail = (from v in db.students
        //                    where studentid == v.stid
        //                    select v.studentemail).firstordefault();
        //    return stuemail;
        //}





    }
}