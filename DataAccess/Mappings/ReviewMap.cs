using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    internal class ReviewMap : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews", "dbo");

            builder.HasKey(x => x.ReviewId);

            builder.Property(x => x.ReviewId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Excerpt)
                   .IsRequired()
                   .HasMaxLength(500)
                   .ValueGeneratedNever();

            builder.Property(x => x.BeerRating)
                   .IsRequired()
                   .HasPrecision(2, 1)
                   .ValueGeneratedNever();

            builder.Property(x => x.AmenitiesRating)
                   .IsRequired()
                   .HasPrecision(2, 1)
                   .ValueGeneratedNever();

            builder.Property(x => x.AtmosphereRating)
                   .IsRequired()
                   .HasPrecision(2, 1)
                   .ValueGeneratedNever();

            builder.Property(x => x.ValueForMoneyRating)
                   .IsRequired()
                   .HasPrecision(2, 1)
                   .ValueGeneratedNever();
            
            builder.Property(x => x.DateOfReview)
                   .IsRequired()
                   .ValueGeneratedNever();

            builder.HasOne(r => r.Venue)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.VenueId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
