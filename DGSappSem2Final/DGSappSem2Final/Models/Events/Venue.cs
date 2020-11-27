using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Events
{
    public class Venue
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int venueId { get; set; }

        [Required]
        [Display(Name = "Venue name")]
        public string venueName { get; set; }

        //[Required]
        //[Display(Name = "Location")]

        //public string Location { get; set; }

        //[Display(Name = "Event Price"), DataType(DataType.Currency)]
        //public decimal? price { get; set; }
        //public virtual ICollection<Event> Events { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        [Range(2, 2000)]
        public int capacity { get; set; }

        public Status Status { get; set; }
        //  public virtual ICollection<BookEvent> BookEvents { get; set; }
    }

    public enum Status
    {
        Avaliable,
        Booked
    }
}

    
    