﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QRCodeAttendance.Infrastructure.Data;

#nullable disable

namespace QRCodeAttendance.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240527075232_4")]
    partial class _4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlAttendace", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<TimeSpan>("CheckInTime")
                        .HasColumnType("interval");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("boolean");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendaces");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlCompany", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ImagesId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<TimeSpan>("MaxLateTime")
                        .HasColumnType("interval");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("ImagesId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlDepartment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlPosition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsDeleted = false,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            IsDeleted = false,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 3L,
                            IsDeleted = false,
                            Name = "User"
                        },
                        new
                        {
                            Id = 4L,
                            IsDeleted = false,
                            Name = "Guest"
                        });
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ImagesId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWoman")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("PositionId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("VerifyToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ImagesId");

                    b.HasIndex("PositionId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "admin@gmail.com",
                            FullName = "Admin",
                            IsDeleted = false,
                            IsVerified = true,
                            IsWoman = false,
                            Password = "admin",
                            Phone = "",
                            RoleId = 1L,
                            VerifyToken = ""
                        });
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlAttendace", b =>
                {
                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlCompany", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlDepartment", "Department")
                        .WithMany("Attendances")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlUser", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlCompany", b =>
                {
                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlFile", "Images")
                        .WithMany()
                        .HasForeignKey("ImagesId");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlNotification", b =>
                {
                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlPosition", b =>
                {
                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlDepartment", "Department")
                        .WithMany("Positions")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlToken", b =>
                {
                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlUser", b =>
                {
                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlDepartment", "Department")
                        .WithMany("User")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlFile", "Images")
                        .WithMany()
                        .HasForeignKey("ImagesId");

                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlPosition", "Position")
                        .WithMany("Users")
                        .HasForeignKey("PositionId");

                    b.HasOne("QRCodeAttendance.Domain.Entities.SqlRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Images");

                    b.Navigation("Position");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlDepartment", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Positions");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlPosition", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("QRCodeAttendance.Domain.Entities.SqlUser", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
