using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    internal class VenueMap : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.ToTable("Venues", "dbo");

            builder.HasKey(x => x.VenueId);

            builder.Property(x => x.VenueId)
                   .ValueGeneratedOnAdd();
            
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(300)
                   .ValueGeneratedNever();

            builder.Property(x => x.Latitude)
                   .IsRequired()
                   .HasPrecision(9, 6)
                   .ValueGeneratedNever();

            builder.Property(x => x.Longitude)
                   .IsRequired()
                   .HasPrecision(8, 6)
                   .ValueGeneratedNever();
            
            builder.Property(x => x.Phone)
                   .HasColumnType("nvarchar(50)")
                   .ValueGeneratedNever();

            builder.Property(x => x.Address)
                   .HasColumnType("nvarchar(500)")
                   .ValueGeneratedNever();

            builder.Property(x => x.TwitterHandle)
                   .HasColumnType("nvarchar(50)")
                   .ValueGeneratedNever();
            
            builder.Property(x => x.ThumbnailUrl)
                   .HasColumnType("nvarchar(500)")
                   .ValueGeneratedNever();

            builder.Property(x => x.VenueUrl)
                   .HasColumnType("nvarchar(500)")
                   .ValueGeneratedNever();

            builder.Property(x => x.VenueType)
                   .IsRequired()
                   .ValueGeneratedNever()
                   .HasConversion<int>();

            builder.HasMany(p => p.Reviews)
                   .WithOne(r => r.Venue)
                   .HasForeignKey(r => r.VenueId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Ignore(p => p.Tags);
        }
    }
}
