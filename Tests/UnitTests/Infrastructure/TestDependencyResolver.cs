using Application.Infrastructure;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;

namespace UnitTests.Infrastructure
{
    internal class TestDependencyResolver
    {
        private static readonly IConfiguration? _configuration 
            =  new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static IServiceProvider Resolve()
        {
            var services = new ServiceCollection();

            TestDatabaseCreator.SetupDbContext<BeerQuestDbContext>(services);

            services.AddApplicationServices()
                    .AddDataAccessServices();

            services.AddAutoMapper(Assembly.Load("Application"));

            MockHttpContext(services);

            return services.BuildServiceProvider();
        }

        private static void MockHttpContext(IServiceCollection services)
        {
            services.AddScoped(
                _ =>
                {
                    var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

                    var context = new DefaultHttpContext();

                    mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

                    return mockHttpContextAccessor.Object;
                });
        }
    }
}