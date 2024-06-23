﻿// <auto-generated />
using System;
using ClassLibrary.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Web.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("ClassLibrary.Data.Entities.AuditHistory", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentValues")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("KeyValues")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalValues")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.ToTable("AuditHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
