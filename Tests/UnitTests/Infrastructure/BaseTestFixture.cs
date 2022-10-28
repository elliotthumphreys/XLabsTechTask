using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace UnitTests.Infrastructure
{
    internal class BaseTestFixture
    {
        protected IServiceProvider _serviceProvider { get; private set; } = default!;
        protected BeerQuestDbContext _context { get; private set; } = default!;

        [SetUp]
        public void BaseSetup()
        {
            _serviceProvider = TestDependencyResolver.Resolve();

            _context = _serviceProvider.GetRequiredService<BeerQuestDbContext>();
        }

        [TearDown]
        public void BaseTearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
