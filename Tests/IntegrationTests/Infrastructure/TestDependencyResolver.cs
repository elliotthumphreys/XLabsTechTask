using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Infrastructure
{
    internal class TestDependencyResolver
    {
        private static readonly IConfiguration _configuration 
            =  new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static IServiceProvider Resolve()
        {
            var services = new ServiceCollection();

            TestDatabaseCreator.SetupDbContext<BeerQuestDbContext>(services, _configuration);

            AddHttpContext(services);

            return services.BuildServiceProvider();
        }

        private static void AddHttpContext(IServiceCollection services)
        {
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddHttpClient("webapi", c =>
            {
                c.BaseAddress = new Uri(_configuration["ConnectionStrings:WebApi"]);

                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}