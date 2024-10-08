﻿// <auto-generated />
using System;
using MemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MemoApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MemoApp.Models.Object.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MemoApp.Models.Object.EmployeeJob", b =>
                {
                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("JobId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeJobs");
                });

            modelBuilder.Entity("MemoApp.Models.Object.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("MemoApp.Models.Object.Memo", b =>
                {
                    b.Property<int>("MemoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemoId"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemoId");

                    b.ToTable("Memos");
                });

            modelBuilder.Entity("MemoApp.Models.Object.MemoEmployee", b =>
                {
                    b.Property<int>("MemoId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("Signed")
                        .HasColumnType("bit");

                    b.HasKey("MemoId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("MemoEmployees");
                });

            modelBuilder.Entity("MemoApp.Models.Object.MemoJob", b =>
                {
                    b.Property<int>("MemoId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("MemoId", "JobId");

                    b.HasIndex("JobId");

                    b.ToTable("MemoJobs");
                });

            modelBuilder.Entity("MemoApp.Models.Object.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MemoApp.Models.Object.EmployeeJob", b =>
                {
                    b.HasOne("MemoApp.Models.Object.Employee", "Employee")
                        .WithMany("EmployeeJobs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemoApp.Models.Object.Job", "Job")
                        .WithMany("EmployeeJob")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("MemoApp.Models.Object.MemoEmployee", b =>
                {
                    b.HasOne("MemoApp.Models.Object.Employee", "Employee")
                        .WithMany("MemoEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemoApp.Models.Object.Memo", "Memo")
                        .WithMany("MemoEmployees")
                        .HasForeignKey("MemoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Memo");
                });

            modelBuilder.Entity("MemoApp.Models.Object.MemoJob", b =>
                {
                    b.HasOne("MemoApp.Models.Object.Job", "Job")
                        .WithMany("MemoJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemoApp.Models.Object.Memo", "Memo")
                        .WithMany("MemoJobs")
                        .HasForeignKey("MemoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Memo");
                });

            modelBuilder.Entity("MemoApp.Models.Object.Employee", b =>
                {
                    b.Navigation("EmployeeJobs");

                    b.Navigation("MemoEmployees");
                });

            modelBuilder.Entity("MemoApp.Models.Object.Job", b =>
                {
                    b.Navigation("EmployeeJob");

                    b.Navigation("MemoJobs");
                });

            modelBuilder.Entity("MemoApp.Models.Object.Memo", b =>
                {
                    b.Navigation("MemoEmployees");

                    b.Navigation("MemoJobs");
                });
#pragma warning restore 612, 618
        }
    }
}
