using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Library
{
    public class BookCategory
    {
        [Key]

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}