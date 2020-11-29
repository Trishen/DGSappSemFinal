namespace DGSappSem2Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessments",
                c => new
                    {
                        AssessmentID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AssessmentID);
            
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.BookReservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(),
                        BookName = c.String(),
                        StudentId = c.Int(),
                        StudentName = c.String(),
                        CollectionDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        SMSMessage = c.String(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.BookId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Genre = c.String(),
                        Title = c.String(),
                        Description = c.String(nullable: false),
                        Author = c.String(),
                        Numpages = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StID = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        StudentSurname = c.String(nullable: false),
                        StudentGender = c.String(nullable: false),
                        StudentAddress = c.String(nullable: false),
                        StudentTown = c.String(nullable: false),
                        StudentContact = c.String(nullable: false),
                        StudentGrade = c.String(nullable: false),
                        StudentEmail = c.String(nullable: false),
                        StudentBirthCertURL = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        ParentName = c.String(nullable: false),
                        ParentSurName = c.String(nullable: false),
                        ParentContact = c.String(nullable: false),
                        ParentEmail = c.String(nullable: false),
                        StudentAllowReg = c.Boolean(nullable: false),
                        StaffId = c.Int(),
                        AssignedTeacher = c.String(),
                        ClassId = c.Int(),
                        ClassName = c.String(),
                    })
                .PrimaryKey(t => t.StID)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.StaffId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false),
                        GradeId = c.Int(),
                        GradeName = c.String(nullable: false),
                        MaxNoOfClasses = c.Int(nullable: false),
                        MaxNoOfStudentsPerClass = c.Int(nullable: false),
                        NoOfAssignedStudents = c.Int(nullable: false),
                        StaffId = c.Int(),
                        AssignedTeacher = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.GradeId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(nullable: false),
                        MaxNoOfStudentsInGrade = c.Int(nullable: false),
                        MaxNoOfClasses = c.Int(nullable: false),
                        MaxNoOfStudentsPerClass = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        StaffPositionId = c.Int(),
                        StaffPositionName = c.String(),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.StaffPositions", t => t.StaffPositionId)
                .Index(t => t.StaffPositionId);
            
            CreateTable(
                "dbo.StaffPositions",
                c => new
                    {
                        StaffPositionId = c.Int(nullable: false, identity: true),
                        StaffPositionName = c.String(nullable: false),
                        LimitedPosition = c.Boolean(nullable: false),
                        Limit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaffPositionId);
            
            CreateTable(
                "dbo.ExtraMuralAgeGroups",
                c => new
                    {
                        MuralAgeGroupId = c.Int(nullable: false, identity: true),
                        ExtraMuralId = c.Int(),
                        ExtraMuralName = c.String(nullable: false),
                        AgeGroupName = c.Int(nullable: false),
                        NoAssignedStudents = c.Int(nullable: false),
                        StaffId = c.Int(),
                        AssignedTeacher = c.String(),
                    })
                .PrimaryKey(t => t.MuralAgeGroupId)
                .ForeignKey("dbo.ExtraMurals", t => t.ExtraMuralId)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.ExtraMuralId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.ExtraMurals",
                c => new
                    {
                        MuralId = c.Int(nullable: false, identity: true),
                        MuralName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MuralId);
            
            CreateTable(
                "dbo.Fees",
                c => new
                    {
                        FeeId = c.Int(nullable: false, identity: true),
                        GradeId = c.Int(),
                        GradeName = c.String(nullable: false),
                        GradeFee = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FeeId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.GradeSubjects",
                c => new
                    {
                        GradeSubjectId = c.Int(nullable: false, identity: true),
                        GradeId = c.Int(),
                        GradeName = c.String(),
                        SubjectId = c.Int(),
                        SubjectName = c.String(),
                        NoOfLessonsRequired = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeSubjectId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.GradeId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StaffAttendances",
                c => new
                    {
                        StaffAttendanceId = c.Int(nullable: false, identity: true),
                        StaffAttName = c.String(nullable: false),
                        Staffrecord = c.String(nullable: false),
                        GetDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StaffAttendanceId);
            
            CreateTable(
                "dbo.StaffSubjects",
                c => new
                    {
                        StaffSubjectId = c.Int(nullable: false, identity: true),
                        GradeSubjectId = c.Int(),
                        GradeName = c.String(),
                        SubjectId = c.Int(),
                        SubjectName = c.String(),
                        StaffId = c.Int(),
                        AssignedTeacher = c.String(),
                    })
                .PrimaryKey(t => t.StaffSubjectId)
                .ForeignKey("dbo.GradeSubjects", t => t.SubjectId)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.StaffTimetables",
                c => new
                    {
                        StaffTimetableId = c.Int(nullable: false, identity: true),
                        Registration = c.String(),
                        Monday_ClassSession1 = c.String(),
                        Monday_ClassSession2 = c.String(),
                        Monday_ClassSession3 = c.String(),
                        Monday_ClassSession4 = c.String(),
                        Monday_ClassSession5 = c.String(),
                        Monday_ClassSession6 = c.String(),
                        Tuesday_ClassSession1 = c.String(),
                        Tuesday_ClassSession2 = c.String(),
                        Tuesday_ClassSession3 = c.String(),
                        Tuesday_ClassSession4 = c.String(),
                        Tuesday_ClassSession5 = c.String(),
                        Tuesday_ClassSession6 = c.String(),
                        Wednesday_ClassSession1 = c.String(),
                        Wednesday_ClassSession2 = c.String(),
                        Wednesday_ClassSession3 = c.String(),
                        Wednesday_ClassSession4 = c.String(),
                        Wednesday_ClassSession5 = c.String(),
                        Wednesday_ClassSession6 = c.String(),
                        Thursday_ClassSession1 = c.String(),
                        Thursday_ClassSession2 = c.String(),
                        Thursday_ClassSession3 = c.String(),
                        Thursday_ClassSession4 = c.String(),
                        Thursday_ClassSession5 = c.String(),
                        Thursday_ClassSession6 = c.String(),
                        Friday_ClassSession1 = c.String(),
                        Friday_ClassSession2 = c.String(),
                        Friday_ClassSession3 = c.String(),
                        Friday_ClassSession4 = c.String(),
                        Friday_ClassSession5 = c.String(),
                        Friday_ClassSession6 = c.String(),
                        HasAssignedClasses = c.Boolean(nullable: false),
                        StaffId = c.Int(),
                        StaffName = c.String(),
                        AssignedTeacher = c.String(),
                        TimeTableAssigned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StaffTimetableId)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.StudentFees",
                c => new
                    {
                        StudentFeeId = c.Int(nullable: false, identity: true),
                        FeeId = c.Int(),
                        GradeFee = c.Double(nullable: false),
                        FeeBalance = c.Double(nullable: false),
                        StudentId = c.Int(),
                        StudentName = c.String(),
                        GuardianName = c.String(),
                        GuardianContact = c.String(),
                        GradeId = c.Int(),
                        GradeName = c.String(),
                    })
                .PrimaryKey(t => t.StudentFeeId)
                .ForeignKey("dbo.Fees", t => t.FeeId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.FeeId)
                .Index(t => t.StudentId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.Terms",
                c => new
                    {
                        TermId = c.Int(nullable: false, identity: true),
                        TermName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TermId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VenueBookings",
                c => new
                    {
                        BookEventId = c.Int(nullable: false, identity: true),
                        VenueId = c.Int(),
                        VenueName = c.String(),
                        ClassId = c.Int(),
                        ClassName = c.String(),
                        GradeId = c.Int(),
                        GradeName = c.String(),
                        DateBookinFor = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        BookingReason = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookEventId)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.VenueId)
                .Index(t => t.ClassId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        venueId = c.Int(nullable: false, identity: true),
                        venueName = c.String(nullable: false),
                        capacity = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.venueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenueBookings", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueBookings", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.VenueBookings", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentFees", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentFees", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.StudentFees", "FeeId", "dbo.Fees");
            DropForeignKey("dbo.StaffTimetables", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.StaffSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StaffSubjects", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.StaffSubjects", "SubjectId", "dbo.GradeSubjects");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GradeSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.GradeSubjects", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Fees", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.ExtraMuralAgeGroups", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.ExtraMuralAgeGroups", "ExtraMuralId", "dbo.ExtraMurals");
            DropForeignKey("dbo.BookReservations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Classes", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Staffs", "StaffPositionId", "dbo.StaffPositions");
            DropForeignKey("dbo.Classes", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.BookReservations", "BookId", "dbo.Books");
            DropIndex("dbo.VenueBookings", new[] { "GradeId" });
            DropIndex("dbo.VenueBookings", new[] { "ClassId" });
            DropIndex("dbo.VenueBookings", new[] { "VenueId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StudentFees", new[] { "GradeId" });
            DropIndex("dbo.StudentFees", new[] { "StudentId" });
            DropIndex("dbo.StudentFees", new[] { "FeeId" });
            DropIndex("dbo.StaffTimetables", new[] { "StaffId" });
            DropIndex("dbo.StaffSubjects", new[] { "StaffId" });
            DropIndex("dbo.StaffSubjects", new[] { "SubjectId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.GradeSubjects", new[] { "SubjectId" });
            DropIndex("dbo.GradeSubjects", new[] { "GradeId" });
            DropIndex("dbo.Fees", new[] { "GradeId" });
            DropIndex("dbo.ExtraMuralAgeGroups", new[] { "StaffId" });
            DropIndex("dbo.ExtraMuralAgeGroups", new[] { "ExtraMuralId" });
            DropIndex("dbo.Staffs", new[] { "StaffPositionId" });
            DropIndex("dbo.Classes", new[] { "StaffId" });
            DropIndex("dbo.Classes", new[] { "GradeId" });
            DropIndex("dbo.Students", new[] { "ClassId" });
            DropIndex("dbo.Students", new[] { "StaffId" });
            DropIndex("dbo.BookReservations", new[] { "StudentId" });
            DropIndex("dbo.BookReservations", new[] { "BookId" });
            DropTable("dbo.Venues");
            DropTable("dbo.VenueBookings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Terms");
            DropTable("dbo.StudentFees");
            DropTable("dbo.StaffTimetables");
            DropTable("dbo.StaffSubjects");
            DropTable("dbo.StaffAttendances");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Subjects");
            DropTable("dbo.GradeSubjects");
            DropTable("dbo.Fees");
            DropTable("dbo.ExtraMurals");
            DropTable("dbo.ExtraMuralAgeGroups");
            DropTable("dbo.StaffPositions");
            DropTable("dbo.Staffs");
            DropTable("dbo.Grades");
            DropTable("dbo.Classes");
            DropTable("dbo.Students");
            DropTable("dbo.Books");
            DropTable("dbo.BookReservations");
            DropTable("dbo.BookCategories");
            DropTable("dbo.Assessments");
        }
    }
}
