﻿// <auto-generated />
using System;
using KartverketProsjekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    [DbContext(typeof(KartverketDbContext))]
    [Migration("20241125094034_DeleteBehaviourChange")]
    partial class DeleteBehaviourChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "614e8a9e-2d90-4d6e-b632-86c77263854a",
                            Email = "sysadmin@test.com",
                            EmailConfirmed = false,
                            FirstName = "System",
                            LastName = "Administrator",
                            LockoutEnabled = false,
                            NormalizedEmail = "SYSADMIN@TEST.COM",
                            NormalizedUserName = "SYSADMIN@TEST.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAENNom0dpnqT4LDfhtqex2zvwTH89BzPp2QQt6/ETinVn8y/eKC5yN8cPOvKkfz/Iiw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8ec91586-7126-4375-bfca-8f52e67d3f57",
                            TwoFactorEnabled = false,
                            UserName = "sysadmin@test.com"
                        },
                        new
                        {
                            Id = "3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c55141a2-e732-42fe-b02a-d1061f34b3ce",
                            Email = "casehandler@test.com",
                            EmailConfirmed = false,
                            FirstName = "Test",
                            LastName = "CaseHandler",
                            LockoutEnabled = false,
                            NormalizedEmail = "CASEHANDLER@TEST.COM",
                            NormalizedUserName = "CASEHANDLER@TEST.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAECZ33Y5f+kw0c5do378h1H+Dx6SNkPTtdjsq9bUHyrmUQiw0j7ZGAp+aiSjx9e7j0g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "91084da7-052a-4d84-958e-7ece618b887f",
                            TwoFactorEnabled = false,
                            UserName = "casehandler@test.com"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4837a0c2-b727-4c81-bd91-6dcc4568bb61",
                            Email = "submitter@test.com",
                            EmailConfirmed = false,
                            FirstName = "Test",
                            LastName = "Submitter",
                            LockoutEnabled = false,
                            NormalizedEmail = "SUBMITTER@TEST.COM",
                            NormalizedUserName = "SUBMITTER@TEST.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEE7p70Wi7QJXYnC88YUmW9lr7ufsxbffNJd773rOeT3khn0Gj/NjVyLS+BaOkjt2Iw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "25d763e7-432e-4a77-85ad-a69904ae3a48",
                            TwoFactorEnabled = false,
                            UserName = "submitter@test.com"
                        });
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.AttachmentModel", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AttachmentId"));

                    b.Property<string>("FilePath")
                        .HasColumnType("longtext");

                    b.Property<int>("MapReportId")
                        .HasColumnType("int");

                    b.HasKey("AttachmentId");

                    b.HasIndex("MapReportId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapLayerModel", b =>
                {
                    b.Property<int>("MapLayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MapLayerId"));

                    b.Property<string>("MapLayerType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MapLayerId");

                    b.ToTable("MapLayer");

                    b.HasData(
                        new
                        {
                            MapLayerId = 1,
                            MapLayerType = "Fargekart"
                        },
                        new
                        {
                            MapLayerId = 2,
                            MapLayerType = "Gråtonekart"
                        },
                        new
                        {
                            MapLayerId = 3,
                            MapLayerType = "Turkart"
                        },
                        new
                        {
                            MapLayerId = 4,
                            MapLayerType = "Sjøkart"
                        });
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportModel", b =>
                {
                    b.Property<int>("MapReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MapReportId"));

                    b.Property<string>("CaseHandlerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("County")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("GeoJsonString")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MapLayerId")
                        .HasColumnType("int");

                    b.Property<int>("MapReportStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Municipality")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SubmitterId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("MapReportId");

                    b.HasIndex("CaseHandlerId");

                    b.HasIndex("MapLayerId");

                    b.HasIndex("MapReportStatusId");

                    b.HasIndex("SubmitterId");

                    b.ToTable("MapReport");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportStatusModel", b =>
                {
                    b.Property<int>("MapReportStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MapReportStatusId"));

                    b.Property<string>("StatusDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MapReportStatusId");

                    b.ToTable("MapReportStatus");

                    b.HasData(
                        new
                        {
                            MapReportStatusId = 1,
                            StatusDescription = "Pending"
                        },
                        new
                        {
                            MapReportStatusId = 2,
                            StatusDescription = "In Progress"
                        },
                        new
                        {
                            MapReportStatusId = 3,
                            StatusDescription = "Completed"
                        },
                        new
                        {
                            MapReportStatusId = 4,
                            StatusDescription = "Rejected"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "1",
                            Name = "System Administrator",
                            NormalizedName = "SYSADMIN"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "2",
                            Name = "Case Handler",
                            NormalizedName = "CASEHANDLER"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "3",
                            Name = "Submitter",
                            NormalizedName = "SUBMITTER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "1",
                            RoleId = "3"
                        },
                        new
                        {
                            UserId = "1",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "3",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "2",
                            RoleId = "3"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.AttachmentModel", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapReportModel", "MapReport")
                        .WithMany("Attachments")
                        .HasForeignKey("MapReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MapReport");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportModel", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.ApplicationUser", "CaseHandler")
                        .WithMany()
                        .HasForeignKey("CaseHandlerId");

                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapLayerModel", "MapLayer")
                        .WithMany()
                        .HasForeignKey("MapLayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapReportStatusModel", "MapReportStatus")
                        .WithMany()
                        .HasForeignKey("MapReportStatusId")
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.ApplicationUser", "Submitter")
                        .WithMany()
                        .HasForeignKey("SubmitterId");

                    b.Navigation("CaseHandler");

                    b.Navigation("MapLayer");

                    b.Navigation("MapReportStatus");

                    b.Navigation("Submitter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportModel", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
