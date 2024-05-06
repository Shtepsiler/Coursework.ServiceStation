﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PARTS.DAL.Data;

#nullable disable

namespace PARTS.DAL.Migrations
{
    [DbContext(typeof(PartsDBContext))]
    [Migration("20240417114200_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categoties");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.CategoryImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HashFileContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.Property<string>("SourceContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("CategoryImages");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Part", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FitNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsMadeToOrder")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsUniversal")
                        .HasColumnType("bit");

                    b.Property<string>("ManufacturerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartAttributes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriceRegular")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.PartImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HashFileContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.Property<string>("SourceContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PartId")
                        .IsUnique();

                    b.ToTable("PartImages");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Engine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cylinders")
                        .HasColumnType("int");

                    b.Property<int>("Liter")
                        .HasColumnType("int");

                    b.Property<Guid?>("MakeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.HasIndex("SubModelId");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Make", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MakeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.SubModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("SubModels");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EngineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MakeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EngineId");

                    b.HasIndex("MakeId");

                    b.HasIndex("ModelId");

                    b.HasIndex("SubModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.CategoryImage", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Item.Category", "Category")
                        .WithOne("CategoryImage")
                        .HasForeignKey("PARTS.DAL.Entities.Item.CategoryImage", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Part", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Item.Brand", "Brand")
                        .WithMany("Parts")
                        .HasForeignKey("BrandId");

                    b.HasOne("PARTS.DAL.Entities.Item.Category", "Category")
                        .WithMany("Parts")
                        .HasForeignKey("CategoryId");

                    b.HasOne("PARTS.DAL.Entities.Vehicle.Vehicle", null)
                        .WithMany("Parts")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.PartImage", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Item.Part", "Part")
                        .WithOne("PartImage")
                        .HasForeignKey("PARTS.DAL.Entities.Item.PartImage", "PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Engine", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Vehicle.Make", "Make")
                        .WithMany("Engines")
                        .HasForeignKey("MakeId");

                    b.HasOne("PARTS.DAL.Entities.Vehicle.SubModel", "SubModel")
                        .WithMany("Engines")
                        .HasForeignKey("SubModelId");

                    b.Navigation("Make");

                    b.Navigation("SubModel");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Model", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Vehicle.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId");

                    b.Navigation("Make");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.SubModel", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Vehicle.Model", "Model")
                        .WithMany("SubModels")
                        .HasForeignKey("ModelId");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Vehicle", b =>
                {
                    b.HasOne("PARTS.DAL.Entities.Vehicle.Engine", "Engine")
                        .WithMany("Vehicles")
                        .HasForeignKey("EngineId");

                    b.HasOne("PARTS.DAL.Entities.Vehicle.Make", "Make")
                        .WithMany("Vehicles")
                        .HasForeignKey("MakeId");

                    b.HasOne("PARTS.DAL.Entities.Vehicle.Model", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("ModelId");

                    b.HasOne("PARTS.DAL.Entities.Vehicle.SubModel", "SubModel")
                        .WithMany("Vehicles")
                        .HasForeignKey("SubModelId");

                    b.Navigation("Engine");

                    b.Navigation("Make");

                    b.Navigation("Model");

                    b.Navigation("SubModel");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Brand", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Category", b =>
                {
                    b.Navigation("CategoryImage");

                    b.Navigation("Parts");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Item.Part", b =>
                {
                    b.Navigation("PartImage");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Engine", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Make", b =>
                {
                    b.Navigation("Engines");

                    b.Navigation("Models");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Model", b =>
                {
                    b.Navigation("SubModels");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.SubModel", b =>
                {
                    b.Navigation("Engines");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("PARTS.DAL.Entities.Vehicle.Vehicle", b =>
                {
                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}
