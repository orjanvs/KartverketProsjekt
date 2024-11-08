using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Data
{
    public class KartverketDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public KartverketDbContext(DbContextOptions<KartverketDbContext> options) : base(options)
        {

        }

        // Table for MapReportModel
        public DbSet<MapReportModel> MapReport { get; set; }
        public DbSet<MapLayerModel> MapLayer { get; set; }
        public DbSet<MapReportStatusModel> MapReportStatus { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<UserRoleModel> UserRole { get; set; }
        public DbSet<DialogueModel> Dialogue { get; set; }
        public DbSet<AttachmentModel> Attachment { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Section for Identity tables

            base.OnModelCreating(modelBuilder);

            var sysAdminRoleId = "1";
            var caseHandlerRoleId = "2";
            var submitterRoleId = "3";

            // Seed roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "System Administrator",
                    NormalizedName = "SYSADMIN",
                    Id = sysAdminRoleId,
                    ConcurrencyStamp = sysAdminRoleId
                },
                new IdentityRole
                {
                    Name = "Case Handler",
                    NormalizedName = "CASEHANDLER",
                    Id = caseHandlerRoleId,
                    ConcurrencyStamp = caseHandlerRoleId
                },
                new IdentityRole
                {
                    Name = "Submitter",
                    NormalizedName = "SUBMITTER",
                    Id = submitterRoleId,
                    ConcurrencyStamp = submitterRoleId
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);


            // Seed SYSADMIN

            var sysAdminId = "1";
            var sysAdminUser = new ApplicationUser
            {
                Id = sysAdminId,
                UserName = "sysadmin@test.com",
                NormalizedUserName = "sysadmin@test.com".ToUpper(),
                Email = "sysadmin@test.com",
                NormalizedEmail = "sysadmin@test.com".ToUpper(),
                FirstName = "System",
                LastName = "Administrator"
            };

            sysAdminUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(sysAdminUser, "Test@123");

            modelBuilder.Entity<ApplicationUser>().HasData(sysAdminUser);

            // Add all roles to SYSADMIN
            var sysAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = sysAdminRoleId,
                    UserId = sysAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = submitterRoleId,
                    UserId = sysAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = caseHandlerRoleId,
                    UserId = sysAdminId
                }
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(sysAdminRoles);

            // Seed Case Handler
            var caseHandlerId = "3";
            var caseHandlerUser = new ApplicationUser
            {
                Id = caseHandlerId,
                UserName = "casehandler@test.com",
                NormalizedUserName = "CASEHANDLER@TEST.COM",
                Email = "casehandler@test.com",
                NormalizedEmail = "CASEHANDLER@TEST.COM",
                FirstName = "Test",
                LastName = "CaseHandler"
            };

            caseHandlerUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(caseHandlerUser, "CaseHandler@123");

            modelBuilder.Entity<ApplicationUser>().HasData(caseHandlerUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = caseHandlerRoleId,
                    UserId = caseHandlerId
                }
            );

            //// Seed second case handler
            //var caseHandlerId2 = "4";
            //var caseHandlerUser2 = new ApplicationUser
            //{
            //    Id = caseHandlerId2,
            //    UserName = "ch2@test.com",
            //    NormalizedUserName = "CH2@TEST.COM",
            //    Email = "ch2@test.com",
            //    NormalizedEmail = "CH2@TEST.COM",
            //    FirstName = "Test2",
            //    LastName = "CaseHandler2"
            //};

            //caseHandlerUser2.PasswordHash = new PasswordHasher<ApplicationUser>()
            //    .HashPassword(caseHandlerUser2, "CaseHandler2@123");

            //modelBuilder.Entity<ApplicationUser>().HasData(caseHandlerUser2);

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        RoleId = caseHandlerRoleId,
            //        UserId = caseHandlerId2
            //    }
            //);

            // Seed Submitter
            var submitterId = "2";
            var submitterUser = new ApplicationUser
            {
                Id = submitterId,
                UserName = "submitter@test.com",
                NormalizedUserName = "SUBMITTER@TEST.COM",
                Email = "submitter@test.com",
                NormalizedEmail = "SUBMITTER@TEST.COM",
                FirstName = "Test",
                LastName = "Submitter"
            };

            submitterUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(submitterUser, "Submitter@123");

            modelBuilder.Entity<ApplicationUser>().HasData(submitterUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = submitterRoleId,
                    UserId = submitterId
                }
            );


            // Primary keys

            modelBuilder.Entity<MapReportModel>()
                .HasKey(m => m.MapReportId);
            modelBuilder.Entity<MapLayerModel>()
                .HasKey(ml => ml.MapLayerId);
            modelBuilder.Entity<MapReportStatusModel>()
                .HasKey(ms => ms.MapReportStatusId);
            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<UserRoleModel>()
                .HasKey(ur => ur.UserRoleId);
            modelBuilder.Entity<DialogueModel>()
                .HasKey(d => d.DialogueId);
            modelBuilder.Entity<AttachmentModel>()
                .HasKey(a => a.AttachmentId);

            // Foreign keys and relationships

            // MapReportModel relationships
            modelBuilder.Entity<MapReportModel>()
                .HasOne(m => m.Submitter)
                .WithMany()
                .HasForeignKey(m => m.SubmitterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MapReportModel>()
                .HasOne(m => m.CaseHandler)
                .WithMany()
                .HasForeignKey(m => m.CaseHandlerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MapReportModel>()
                .HasOne(m => m.MapLayer)
                .WithMany()
                .HasForeignKey(m => m.MapLayerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MapReportModel>()
                .HasOne(m => m.MapReportStatus)
                .WithMany()
                .HasForeignKey(m => m.MapReportStatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Relasjon mellom MapReportModel og AttachmentModel
            modelBuilder.Entity<MapReportModel>()
                .HasMany(m => m.Attachments) // MapReport har mange vedlegg
                .WithOne(a => a.MapReport)    // Vedlegg har én MapReport
                .HasForeignKey(a => a.MapReportId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // UserModel relationship with UserRoleModel
            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.UserRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // DialogueModel relationships
            modelBuilder.Entity<DialogueModel>()
                .HasOne(d => d.MapReport)
                .WithMany()
                .HasForeignKey(d => d.MapReportId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DialogueModel>()
                .HasOne(d => d.Sender)
                .WithMany()
                .HasForeignKey(d => d.SenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DialogueModel>()
                .HasOne(d => d.Recipient)
                .WithMany()
                .HasForeignKey(d => d.RecipientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

         

            // Seed data for MapLayerModel
            modelBuilder.Entity<MapLayerModel>().HasData(
                new MapLayerModel { MapLayerId = 1, MapLayerType = "Fargekart" },
                new MapLayerModel { MapLayerId = 2, MapLayerType = "Gråtonekart" },
                new MapLayerModel { MapLayerId = 3, MapLayerType = "Turkart" },
                new MapLayerModel { MapLayerId = 4, MapLayerType = "Sjøkart" }
            );

            // Seed data for MapReportStatusModel
            modelBuilder.Entity<MapReportStatusModel>().HasData(
                new MapReportStatusModel { MapReportStatusId = 1, StatusDescription = "Pending" },
                new MapReportStatusModel { MapReportStatusId = 2, StatusDescription = "In Progress" },
                new MapReportStatusModel { MapReportStatusId = 3, StatusDescription = "Completed" },
                new MapReportStatusModel { MapReportStatusId = 4, StatusDescription = "Rejected" }
            );

            // Seed data for UserRoleModel
            modelBuilder.Entity<UserRoleModel>().HasData(
                new UserRoleModel { UserRoleId = 1, UserRoleName = "Systemadministrator", IsPrioritised = false },
                new UserRoleModel { UserRoleId = 2, UserRoleName = "Saksbehandler", IsPrioritised = false },
                new UserRoleModel { UserRoleId = 3, UserRoleName = "Innmelder", IsPrioritised = false }
            );


            // Test seed data for users
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    UserId = 1,
                    FirstName = "Default",
                    LastName = "User",
                    Email = "default@example.com",
                    PhoneNumber = "0000000000",
                    UserRoleId = 2 // Assume role with this ID exists
                },
                new UserModel
                {
                    UserId = 2,
                    FirstName = "Test",
                    LastName = "Submitter",
                    Email = "submitter@example.com",
                    PhoneNumber = "1100000000",
                    UserRoleId = 3 // Assume role with this ID exists
                },
                new UserModel
                {
                    UserId = 3,
                    FirstName = "Test",
                    LastName = "CaseHandler",
                    Email = "casehandler@example.com",
                    PhoneNumber = "1200000000",
                    UserRoleId = 2 // Assume role with this ID exists
                }
            );

            


        }
    }


}

