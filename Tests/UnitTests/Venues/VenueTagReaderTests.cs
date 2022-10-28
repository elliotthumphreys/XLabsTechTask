using Application.Exceptions;
using Application.Venues;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Venues
{
    [TestFixture]
    internal class VenueTagReaderTests : BaseTestFixture
    {
        [Test]
        public void GetById_ThrowsNotFound_WhenVenueDoesNotExist()
        {
            // arrange
            const int VENUE_ID_THAT_DOES_NOT_EXIST = 1;

            var venueTagReader = _serviceProvider.GetRequiredService<IReadVenueTag>();

            // act & assert
            Assert.Throws<NotFoundException>(() => venueTagReader.GetById(VENUE_ID_THAT_DOES_NOT_EXIST));
        }

        [Test]
        public void GetById_ReturnsAllTagsForVenue()
        {
            // arrange
            var venueTagReader = _serviceProvider.GetRequiredService<IReadVenueTag>();

            var tag = new Tag("tag 1");
            var tag1 = new Tag("tag 2");
            var tag2 = new Tag("tag 3");

            var venue = new Venue(1.1,
                                  1.2,
                                  "test venue",
                                  "https://exmaple.com",
                                  "https://exmaple.com",
                                  "07388000100",
                                  "test venue twitter handle",
                                  "Test, test, TEST",
                                  VenueType.Pub,
                                  new List<Tag>
                                  {
                                      tag,
                                      tag1,
                                      tag2
                                  });
            
            _context.Add(venue);
            _context.SaveChanges();

            // act
            var venueTags = venueTagReader.GetById(venue.VenueId);

            // assert
            Assert.AreEqual(3, venueTags.Count);
        }
    }

    [TestFixture]
    internal class VenueTagUpdaterTests : BaseTestFixture
    {
        [Test]
        public void Update_ThrowsNotFound_WhenVenueDoesNotExist()
        {
            // arrange
            const int VENUE_ID_THAT_DOES_NOT_EXIST = 1;

            var venueTagUpdater = _serviceProvider.GetRequiredService<IUpdateVenueTag>();

            // act & assert
            Assert.Throws<NotFoundException>(() => venueTagUpdater.Update(VENUE_ID_THAT_DOES_NOT_EXIST, Array.Empty<int>()));
        }

        [Test]
        public void Update_ThrowsNotFound_WhenTagDoesNotExist()
        {
            // arrange
            const int TAG_ID_THAT_DOES_NOT_EXIST = 2;

            var venueTagUpdater = _serviceProvider.GetRequiredService<IUpdateVenueTag>();

            var tag = new Tag("tag 1");

            var venue = new Venue(1.1,
                                  1.2,
                                  "test venue",
                                  "https://exmaple.com",
                                  "https://exmaple.com",
                                  "07388000100",
                                  "test venue twitter handle",
                                  "Test, test, TEST",
                                  VenueType.Pub,
                                  Array.Empty<Tag>());
            
            _context.Add(venue);
            _context.Add(tag);
            _context.SaveChanges();

            var tagIds = new List<int>{ tag.TagId, TAG_ID_THAT_DOES_NOT_EXIST };

            // act & assert
            Assert.Throws<NotFoundException>(() => venueTagUpdater.Update(venue.VenueId, tagIds));
        }

        [Test]
        public void Update_RemovesTagsNotIncludedInRequest()
        {
            // arrange
            var venueTagUpdater = _serviceProvider.GetRequiredService<IUpdateVenueTag>();

            var existingVenueTag = new Tag("tag 1");
            var newVenueTag = new Tag("tag 2");

            var venue = new Venue(1.1,
                                  1.2,
                                  "test venue",
                                  "https://exmaple.com",
                                  "https://exmaple.com",
                                  "07388000100",
                                  "test venue twitter handle",
                                  "Test, test, TEST",
                                  VenueType.Pub,
                                  new List<Tag> { existingVenueTag });
            
            _context.Add(venue);
            _context.Add(newVenueTag);
            _context.SaveChanges();

            var tagIds = new List<int>{ newVenueTag.TagId };

            // act
            var response = venueTagUpdater.Update(venue.VenueId, tagIds);

            // assert
            Assert.AreEqual(1, response.Count);
            Assert.AreEqual(newVenueTag.TagId, response.First().TagId);
            Assert.AreEqual(newVenueTag.Name, response.First().Name);

            // assert venue tag records have been updated in db
            Assert.IsFalse(_context.Set<VenueTag>().Any(x => x.VenueId == venue.VenueId && x.TagId == existingVenueTag.TagId));
            Assert.IsTrue(_context.Set<VenueTag>().Any(x => x.VenueId == venue.VenueId && x.TagId == newVenueTag.TagId));
            
            // assert tag has not been removed
            Assert.IsTrue(_context.Set<Tag>().Any(x => x.TagId == existingVenueTag.TagId));
        }
    }
}
