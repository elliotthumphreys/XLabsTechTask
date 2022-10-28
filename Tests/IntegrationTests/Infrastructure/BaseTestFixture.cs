using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.Infrastructure
{
    internal class BaseTestFixture
    {
        protected IServiceProvider _serviceProvider { get; private set; } = default!;
        protected BeerQuestDbContext _context { get; private set; } = default!;
        protected HttpClient _httpClient { get; private set; } = default!;

        [SetUp]
        [OneTimeSetUp]
        public void BaseSetup() => SetUp();

        [TearDown]
        [OneTimeTearDown]
        public void BaseTearDown() => TearDown();

        private void SetUp()
        {
            _serviceProvider = TestDependencyResolver.Resolve();

            _context = _serviceProvider.GetRequiredService<BeerQuestDbContext>();

            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            _httpClient = httpClientFactory.CreateClient("webapi");
        }

        private void TearDown()
        {
            _context.ChangeTracker.Clear();
            _context.Dispose();

            _httpClient.Dispose();
        }
    }
}
