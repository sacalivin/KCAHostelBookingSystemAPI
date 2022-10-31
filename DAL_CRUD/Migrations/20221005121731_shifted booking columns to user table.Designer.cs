﻿// <auto-generated />
using System;
using DAL_CRUD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL_CRUD.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221005121731_shifted booking columns to user table")]
    partial class shiftedbookingcolumnstousertable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ArmenityRentAlternative", b =>
                {
                    b.Property<int>("ArmenitiesId")
                        .HasColumnType("int");

                    b.Property<int>("RentAlternativesId")
                        .HasColumnType("int");

                    b.HasKey("ArmenitiesId", "RentAlternativesId");

                    b.HasIndex("RentAlternativesId");

                    b.ToTable("ArmenityRentAlternative");
                });

            modelBuilder.Entity("DAL_CRUD.Models.Armenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Armenities");
                });

            modelBuilder.Entity("DAL_CRUD.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckinDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HostelId")
                        .HasColumnType("int");

                    b.Property<string>("ModeOfPayment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Request")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DAL_CRUD.Models.Hostel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hostels");
                });

            modelBuilder.Entity("DAL_CRUD.Models.RentAlternative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("HostelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.ToTable("RentAlternatives");
                });

            modelBuilder.Entity("DAL_CRUD.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Compus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HostelId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModeOfPayment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Request")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArmenityRentAlternative", b =>
                {
                    b.HasOne("DAL_CRUD.Models.Armenity", null)
                        .WithMany()
                        .HasForeignKey("ArmenitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL_CRUD.Models.RentAlternative", null)
                        .WithMany()
                        .HasForeignKey("RentAlternativesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL_CRUD.Models.Book", b =>
                {
                    b.HasOne("DAL_CRUD.Models.Hostel", "Hostel")
                        .WithMany("Books")
                        .HasForeignKey("HostelId");

                    b.Navigation("Hostel");
                });

            modelBuilder.Entity("DAL_CRUD.Models.RentAlternative", b =>
                {
                    b.HasOne("DAL_CRUD.Models.Hostel", "Hostel")
                        .WithMany("RentAlternatives")
                        .HasForeignKey("HostelId");

                    b.Navigation("Hostel");
                });

            modelBuilder.Entity("DAL_CRUD.Models.User", b =>
                {
                    b.HasOne("DAL_CRUD.Models.Hostel", "Hostel")
                        .WithMany("Users")
                        .HasForeignKey("HostelId");

                    b.Navigation("Hostel");
                });

            modelBuilder.Entity("DAL_CRUD.Models.Hostel", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("RentAlternatives");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
