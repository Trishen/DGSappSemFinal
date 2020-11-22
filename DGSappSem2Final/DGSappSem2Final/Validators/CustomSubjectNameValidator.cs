using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomSubjectNameValidator : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override ValidationResult IsValid( object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (!db.Subjects.Any(x => x.SubjectName.Equals(name)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please Enter a Valid Subject Name. Subject Name must be unique");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}