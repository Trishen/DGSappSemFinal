using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Library
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("BookId")]
         public int BookId { get; set; }
        
        public string Genre { get; set; }
       
        [DisplayName("Book Title")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        //[Display(Name = "Picture")]
        //public byte[] Picture { get; set; }
        public string Author { get; set; }
        public int Numpages { get; set; }
        public string Status { get; set; }

    }
}