using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DGSappSem2Final.Models.Student
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Enter your id number")]
        [Display(Name = "Id Number")]
        public int StID { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [Display(Name = "First Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        [Display(Name = "Surname")]
        public string StudentSurname { get; set; }

        [Required(ErrorMessage = "Select your gender")]
        [Display(Name = "Gender")]
        public string StudentGender { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        [Display(Name = "Address")]
        public string StudentAddress { get; set; }

        [Required(ErrorMessage = "Enter contact number"), DataType(DataType.PhoneNumber)]
        [Display(Name = "Student Contact Number")]
        [Phone]
        public string StudentContact { get; set; }

        [Required(ErrorMessage = "Enter Student Grade")]
        [Display(Name = "Grade")]
        public string StudentGrade { get; set; }

        [Required(ErrorMessage = "Enter your email"), DataType(DataType.EmailAddress)]
        [Display(Name = "Student Email")]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Please enter your date of birth "), DataType(DataType.Date)]
        [Display(Name = " Student Date of Birth ")]
        public string StudentBirthCertURL { get; set; }

        //[Required(ErrorMessage = " Upload your last official school report")]
        //[Display(Name = "The last official school report card")]
        //public string StudentReportURL { get; set; }

        //[Required(ErrorMessage = " Upload proof of residence")]
        //[Display(Name = "Proof of residence")]
        //public string StudentProofResURL { get; set; }


        //[Display(Name = "Upload your study permit")]
        //public string StudentPermitURL { get; set; }
        [Required(ErrorMessage = "Gaurdian Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a Gaurdian's name")]
        [Display(Name = "Gaurdian Name")]
        public string ParentName { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [Display(Name = "Gaurdian Surname")]
        public string ParentSurName { get; set; }

        [Required(ErrorMessage = "Enter contact number"), DataType(DataType.PhoneNumber)]
        [Display(Name = "Guardian Contact Number")]
        [Phone]
        public string ParentContact { get; set; }

        [Required(ErrorMessage = "Enter your email"), DataType(DataType.EmailAddress)]
        [Display(Name = "Guardian Email")]
        public string ParentEmail { get; set; }

        [Display(Name = "Allow registration")]
        public bool StudentAllowReg { get; set; }

        [ForeignKey("StaffId")]
        public virtual Staff.Staff Staff { get; set; }

        public int? StaffId { get; set; }
        [Display(Name = "Assigned Teacher")]
        public string AssignedTeacher { get; set; }

        [ForeignKey("ClassId")]
        public virtual Models.Classes.Classes Class { get; set; }

        public int? ClassId { get; set; }
        [Display(Name = "Registration Class")]
        public string ClassName { get; set; }

        public IEnumerable<string> GradeNameCollection { get; set; }
    }
}