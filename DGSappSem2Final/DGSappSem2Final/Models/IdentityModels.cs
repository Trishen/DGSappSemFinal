using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DGSappSem2Final.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Staff.StaffPositions> StaffPositions { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Staff.Staff> Staffs { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Grade.Grades> Grades { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Classes.Classes> Classes { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Subject.Subjects> Subjects { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Grade.GradeSubjects> GradeSubjects { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Staff.StaffSubjects> StaffSubjects { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Staff.StaffTimetable> StaffTimetables { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Terms.Term> Terms { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Staff.StaffAttendance> StaffAttendances { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Murals.ExtraMural> ExtraMurals { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Student.Student> Students { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Library.Book> Books { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Library.BookCategory> BookCategories { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Library.BookReservation> BookReservations { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Grade.Fee> Fees { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Student.StudentFees> StudentFees { get; set; }

        public System.Data.Entity.DbSet<DGSappSem2Final.Models.Assements.Assessment> Assessments { get; set; }
    }
}