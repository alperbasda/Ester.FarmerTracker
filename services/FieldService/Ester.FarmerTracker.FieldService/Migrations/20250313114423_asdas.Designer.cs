﻿// <auto-generated />
using System;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ester.FarmerTracker.FieldService.Migrations
{
    [DbContext(typeof(FieldDbContext))]
    [Migration("20250313114423_asdas")]
    partial class asdas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Crops._base.Entities.Crop", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Crops", (string)null);
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Customers._base.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityPlate")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FieldsSquereMeterSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("IdentityNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("MailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SalesRepresantativeUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SalesRepresantativeUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityPlate");

                    b.HasIndex("SalesRepresantativeUserId");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Fields._base.Entities.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityPlate")
                        .HasColumnType("int");

                    b.Property<string>("Coordinate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentCropName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentTotalFertilizerAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SquareMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityPlate");

                    b.HasIndex("CustomerId");

                    b.ToTable("Fields", (string)null);
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities.Harvest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CropId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("HarvestTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CropId");

                    b.HasIndex("FieldId");

                    b.ToTable("Harvests", (string)null);
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Fields._base.Entities.Field", b =>
                {
                    b.HasOne("Ester.FarmerTracker.FieldService.Features.Customers._base.Entities.Customer", "Customer")
                        .WithMany("Fields")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities.Harvest", b =>
                {
                    b.HasOne("Ester.FarmerTracker.FieldService.Features.Crops._base.Entities.Crop", "Crop")
                        .WithMany("Harvests")
                        .HasForeignKey("CropId");

                    b.HasOne("Ester.FarmerTracker.FieldService.Features.Fields._base.Entities.Field", "Field")
                        .WithMany("Harvests")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crop");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Crops._base.Entities.Crop", b =>
                {
                    b.Navigation("Harvests");
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Customers._base.Entities.Customer", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("Ester.FarmerTracker.FieldService.Features.Fields._base.Entities.Field", b =>
                {
                    b.Navigation("Harvests");
                });
#pragma warning restore 612, 618
        }
    }
}
