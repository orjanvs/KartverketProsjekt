using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

            builder.Entity<IdentityRole>().HasData(roles);




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

            builder.Entity<ApplicationUser>().HasData(sysAdminUser);

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

            builder.Entity<IdentityUserRole<string>>().HasData(sysAdminRoles);

            // Seed CASEHANDLER

            // Seed SUBMITTER
        }
    }
}

