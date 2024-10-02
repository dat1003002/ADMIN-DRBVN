﻿// <auto-generated />
using System;
using AspnetCoreMvcFull.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tenxuong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("chieudaicatlon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chieudaicatnho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("doday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khuonlodie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khuonsoiholder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("mahang")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pitch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("quycachloithep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sodaycatduoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("soi1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("soi2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sosoi")
                        .HasColumnType("int");

                    b.Property<string>("thucte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tieuchuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdokeo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tocdomaydun")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Product", b =>
                {
                    b.HasOne("AspnetCoreMvcFull.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AspnetCoreMvcFull.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
