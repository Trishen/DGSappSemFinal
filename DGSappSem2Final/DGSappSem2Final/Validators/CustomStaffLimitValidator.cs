using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomStaffLimitValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid( object value, ValidationContext validationContext)
        {
            var t = (StaffPositions)validationContext.ObjectInstance;
            bool limited = t.LimitedPosition;

            if (limited == true)
            {    
                if ((int)value > 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("When Position is Limited the Position Limit value must be greater than 0");
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}