using DataAccess.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BeerQuestDbContext : DbContext
    {
        private BeerQuestDbContext(){}

        public BeerQuestDbContext(DbContextOptions<BeerQuestDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CustomMappings(modelBuilder);
        }
        
        private void CustomMappings(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VenueMap())
                        .ApplyConfiguration(new VenueTagMap())
                        .ApplyConfiguration(new TagMap())
                        .ApplyConfiguration(new ReviewMap())
                ;
        }
    }
}
