using DGSappSem2Final.Models;
using DGSappSem2Final.Models.Grade;
using DGSappSem2Final.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Validators
{
    public class CustomGradeSubjectValidator : ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var gradSub = (GradeSubjects)value;
                if (gradSub.GradeId == null)
                {
                    return ValidationResult.Success;
                }

                var gradeName = db.Grades.Find(gradSub.GradeId).GradeName;
                var gradeSubject = db.Subjects.Find(gradSub.SubjectId).SubjectName;

                if (!db.GradeSubjects.Any(x => x.GradeName == gradeName && x.SubjectName == gradeSubject))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Combination cannot be added. There is already an existing Grade Subject mapping for Grade: {gradeName}, Subject: {gradeSubject}.");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}