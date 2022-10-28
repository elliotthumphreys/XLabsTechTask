using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    internal class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags", "dbo");

            builder.HasKey(x => x.TagId);

            builder.Property(x => x.TagId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .ValueGeneratedNever();
        }
    }
}
