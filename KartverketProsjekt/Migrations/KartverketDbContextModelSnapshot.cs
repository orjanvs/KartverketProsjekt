﻿// <auto-generated />
using System;
using KartverketProsjekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    [DbContext(typeof(KartverketDbContext))]
    partial class KartverketDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.AttachmentModel", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AttachmentId"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MapReportId")
                        .HasColumnType("int");

                    b.Property<int?>("MapReportModelMapReportId")
                        .HasColumnType("int");

                    b.HasKey("AttachmentId");

                    b.HasIndex("MapReportId");

                    b.HasIndex("MapReportModelMapReportId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.DialogueModel", b =>
                {
                    b.Property<int>("DialogueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DialogueId"));

                    b.Property<int>("MapReportId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("DialogueId");

                    b.HasIndex("MapReportId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Dialogue");
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
                            MapLayerType = "Turkart"
                        },
                        new
                        {
                            MapLayerId = 2,
                            MapLayerType = "Sjøkart"
                        },
                        new
                        {
                            MapLayerId = 3,
                            MapLayerType = "Gråtonekart"
                        });
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportModel", b =>
                {
                    b.Property<int>("MapReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MapReportId"));

                    b.Property<int>("CaseHandlerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GeoJsonString")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MapLayerId")
                        .HasColumnType("int");

                    b.Property<int>("MapReportStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SubmitterId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "default@example.com",
                            FirstName = "Default",
                            LastName = "User",
                            PhoneNumber = "0000000000",
                            UserRoleId = 2
                        },
                        new
                        {
                            UserId = 2,
                            Email = "submitter@example.com",
                            FirstName = "Test",
                            LastName = "Submitter",
                            PhoneNumber = "1100000000",
                            UserRoleId = 3
                        },
                        new
                        {
                            UserId = 3,
                            Email = "casehandler@example.com",
                            FirstName = "Test",
                            LastName = "CaseHandler",
                            PhoneNumber = "1200000000",
                            UserRoleId = 2
                        });
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.UserRoleModel", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserRoleId"));

                    b.Property<bool>("IsPrioritised")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserRoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserRoleId = 1,
                            IsPrioritised = false,
                            UserRoleName = "Systemadministrator"
                        },
                        new
                        {
                            UserRoleId = 2,
                            IsPrioritised = false,
                            UserRoleName = "Saksbehandler"
                        },
                        new
                        {
                            UserRoleId = 3,
                            IsPrioritised = false,
                            UserRoleName = "Innmelder"
                        });
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.AttachmentModel", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapReportModel", "MapReport")
                        .WithMany()
                        .HasForeignKey("MapReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapReportModel", null)
                        .WithMany("Attachments")
                        .HasForeignKey("MapReportModelMapReportId");

                    b.Navigation("MapReport");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.DialogueModel", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapReportModel", "MapReport")
                        .WithMany()
                        .HasForeignKey("MapReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.UserModel", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.UserModel", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MapReport");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportModel", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.UserModel", "CaseHandler")
                        .WithMany()
                        .HasForeignKey("CaseHandlerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapLayerModel", "MapLayer")
                        .WithMany()
                        .HasForeignKey("MapLayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.MapReportStatusModel", "MapReportStatus")
                        .WithMany()
                        .HasForeignKey("MapReportStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartverketProsjekt.Models.DomainModels.UserModel", "Submitter")
                        .WithMany()
                        .HasForeignKey("SubmitterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CaseHandler");

                    b.Navigation("MapLayer");

                    b.Navigation("MapReportStatus");

                    b.Navigation("Submitter");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.UserModel", b =>
                {
                    b.HasOne("KartverketProsjekt.Models.DomainModels.UserRoleModel", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("KartverketProsjekt.Models.DomainModels.MapReportModel", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
