using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    internal class VenueTagMap : IEntityTypeConfiguration<VenueTag>
    {
        public void Configure(EntityTypeBuilder<VenueTag> builder)
        {
            builder.ToTable("VenueTags", "dbo");

            builder.HasKey(x => new { x.VenueId, x.TagId });

            builder.Property(x => x.VenueId)
                   .IsRequired()
                   .ValueGeneratedNever();

            builder.Property(x => x.TagId)
                   .IsRequired()
                   .ValueGeneratedNever();

            builder.HasOne(pt => pt.Venue)
                   .WithMany(p => p.VenueTags)
                   .HasForeignKey(pt => pt.VenueId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(pt => pt.Tag)
                   .WithMany(t => t.VenueTags)
                   .HasForeignKey(pt => pt.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
