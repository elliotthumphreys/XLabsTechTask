using Application.Exceptions;
using Application.Reviews;
using Application.Tags;
using Application.Venues;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Tags
{
    [TestFixture]
    internal class TagDeleterTests : BaseTestFixture
    {
        [Test]
        public void DeleteById_RemovesTag()
        {
            // arrange
            const int TAG_ID = 1;

            var tagDeleter = _serviceProvider.GetRequiredService<IDeleteTag>();

            var tag = new Tag("test tag");

            _context.Add(tag);

            _context.SaveChanges();
            
            // act & assert
            Assert.DoesNotThrow(() => tagDeleter.DeleteById(TAG_ID));

            Assert.IsFalse(_context.Set<Tag>().Any(x => x.TagId == TAG_ID));
        }

        [Test]
        public void DeleteById_RemovesTag_AndAssociatedVenueTags()
        {
            // arrange
            const int VENUE_ID = 1;
            const int TAG_ID = 1;

            var tagDeleter = _serviceProvider.GetRequiredService<IDeleteTag>();
            
            var tag = new Tag("test tag");

            var venue = new Venue(1.1,
                                  1.2,
                                  "test venue",
                                  "https://exmaple.com",
                                  "https://exmaple.com",
                                  "07388000100",
                                  "test venue twitter handle",
                                  "Test, test, TEST",
                                  VenueType.Pub,
                                  new List<Tag>(){ tag });

            _context.Add(venue);
            _context.Add(tag);

            _context.SaveChanges();
            
            // act & assert
            Assert.Throws(typeof(NotFoundException), () => tagDeleter.DeleteById(TAG_ID));

            Assert.IsFalse(_context.Set<Tag>().Any(x => x.TagId == TAG_ID));
            Assert.IsFalse(_context.Set<VenueTag>().Any(x => x.TagId == TAG_ID));
            Assert.IsTrue(_context.Set<Venue>().Any(x => x.VenueId == VENUE_ID));
        }
    }
}
