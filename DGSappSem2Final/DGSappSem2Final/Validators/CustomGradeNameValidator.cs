﻿using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomGradeNameValidator : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override ValidationResult IsValid( object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (!db.Grades.Any(x => x.GradeName.Equals(name)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please Enter a Valid Grade Name. Grade Name must be unique");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}