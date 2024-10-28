using KartverketProsjekt.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Data
{
    public class KartverketDbContext : DbContext
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
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MapReportModel>()
                .HasOne(m => m.CaseHandler)
                .WithMany()
                .HasForeignKey(m => m.CaseHandlerId)
                .IsRequired()
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

            // AttachmentModel relationship with MapReportModel
            modelBuilder.Entity<AttachmentModel>()
                .HasOne(a => a.MapReport)
                .WithMany()
                .HasForeignKey(a => a.MapReportId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for MapLayerModel
            modelBuilder.Entity<MapLayerModel>().HasData(
                new MapLayerModel { MapLayerId = 1, MapLayerType = "Turkart" },
                new MapLayerModel { MapLayerId = 2, MapLayerType = "Sjøkart" },
                new MapLayerModel { MapLayerId = 3, MapLayerType = "Gråtonekart" }
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

