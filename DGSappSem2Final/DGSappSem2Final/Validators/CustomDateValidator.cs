using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomDateValidator : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override ValidationResult IsValid( object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateTimeValue = (DateTime)value;

                if (dateTimeValue > DateTime.Now.Date)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please Enter a Valid Date. Date must be greater than today.");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}