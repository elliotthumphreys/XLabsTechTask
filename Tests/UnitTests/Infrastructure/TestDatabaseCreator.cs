using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace UnitTests.Infrastructure
{
    internal class TestDatabaseCreator
    {
        public static IServiceCollection SetupDbContext<TContext>(IServiceCollection services) where TContext : DbContext
        {
            services.AddDbContext<TContext>(
                options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    options.EnableSensitiveDataLogging();
                });

            services.AddScoped<DbContext, TContext>();

            return services;
        }

        public static DbContext StartSeed(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<DbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        public static void EndSeed(DbContext context)
        {
            context.SaveChanges();

            context.ChangeTracker.Clear();
        }
    }
}
