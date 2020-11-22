using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomExtraMuralNameValidator : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override ValidationResult IsValid( object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (!db.ExtraMurals.Any(x => x.MuralName.Equals(name)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please Enter a Valid Extra Mural Name. Extra Mural Name must be unique");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}