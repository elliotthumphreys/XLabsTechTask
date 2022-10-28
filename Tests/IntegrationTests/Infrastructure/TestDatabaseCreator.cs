using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Infrastructure
{
    internal class TestDatabaseCreator
    {
        public static IServiceCollection SetupDbContext<TContext>(IServiceCollection services, IConfiguration _configuration) where TContext : DbContext
        {

            services.AddDbContext<TContext>(options 
                => options.UseSqlServer(_configuration["ConnectionStrings:BeerQuestDatabaseConnectionString"]));

            services.AddScoped<DbContext, TContext>();

            return services;
        }
    }
}
