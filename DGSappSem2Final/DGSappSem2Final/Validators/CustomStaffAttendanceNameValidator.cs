using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomStaffAttendanceNameValidator : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {

                return new ValidationResult("Could not map QR Code to a Staff Member. Please try again");

            }
            return ValidationResult.Success;
        }
    }
}