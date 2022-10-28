﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(BeerQuestDbContext))]
    partial class BeerQuestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<decimal>("AmenitiesRating")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<decimal>("AtmosphereRating")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<decimal>("BeerRating")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<DateTime>("DateOfReview")
                        .HasColumnType("datetime2");

                    b.Property<string>("Excerpt")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("ValueForMoneyRating")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("VenueId");

                    b.ToTable("Reviews", "dbo");
                });

            modelBuilder.Entity("Domain.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags", "dbo");
                });

            modelBuilder.Entity("Domain.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VenueId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("Latitude")
                        .HasPrecision(9, 6)
                        .HasColumnType("float(9)");

                    b.Property<double>("Longitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("float(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TwitterHandle")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VenueType")
                        .HasColumnType("int");

                    b.Property<string>("VenueUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("VenueId");

                    b.ToTable("Venues", "dbo");
                });

            modelBuilder.Entity("Domain.VenueTag", b =>
                {
                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("VenueId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("VenueTags", "dbo");
                });

            modelBuilder.Entity("Domain.Review", b =>
                {
                    b.HasOne("Domain.Venue", "Venue")
                        .WithMany("Reviews")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("Domain.VenueTag", b =>
                {
                    b.HasOne("Domain.Tag", "Tag")
                        .WithMany("VenueTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Venue", "Venue")
                        .WithMany("VenueTags")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("Domain.Tag", b =>
                {
                    b.Navigation("VenueTags");
                });

            modelBuilder.Entity("Domain.Venue", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("VenueTags");
                });
#pragma warning restore 612, 618
        }
    }
}
