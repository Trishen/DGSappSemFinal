using DGSappSem2Final.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGSappSem2Final.Models.Staff
{
    public class StaffPositions
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StaffPositionId { get; set; }

        [Display(Name = "Position Name")]
        [Required(ErrorMessage = "Enter Staff Position Name")]
        public string StaffPositionName { get; set; }

        [Display(Name = "Is Position Limited")]
        [Required(ErrorMessage = "Staff Position Limitation Field is required to be set")]
        public bool LimitedPosition { get; set; }

        [Display(Name = "Position Limit")]
        [CustomStaffLimitValidator]
        public int Limit { get; set; }

    }
}