using Application.Exceptions;
using Application.Reviews;
using Application.Venues;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Venues
{
    [TestFixture]
    internal class VenueDeleterTests : BaseTestFixture
    {
        [Test]
        public void DeleteById_RemovesVenueAndAllReviews()
        {
            // arrange
            const int VENUE_ID = 1;

            var venueDeleter = _serviceProvider.GetRequiredService<IDeleteVenue>();

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
            
            var review = new Review(2.5M,
                                    3.5M,
                                    4.5M,
                                    1.5M,
                                    "dummy data",
                                    DateTime.Parse("01/01/2020"),
                                    venue
                                    );

            var review2 = new Review(2.5M,
                                    3.5M,
                                    4.5M,
                                    1.5M,
                                    "dummy data",
                                    DateTime.Parse("01/01/2020"),
                                    venue
                                    );

            _context.Add(venue);
            _context.Add(review);
            _context.Add(review2);

            _context.SaveChanges();
            
            // act & assert
            Assert.DoesNotThrow(() => venueDeleter.DeleteById(VENUE_ID));

            Assert.IsFalse(_context.Set<Review>().Any(x => x.VenueId == VENUE_ID));
            Assert.IsFalse(_context.Set<Venue>().Any(x => x.VenueId == VENUE_ID));
        }

        [Test]
        public void DeleteById_TryRemoveVenueThatDoesntExist_ThrowsNotFoundException()
        {
            // arrange
            const int VENUE_ID_THAT_DOESNT_EXIST = 2;
            const int VENUE_ID = 1;

            var venueDeleter = _serviceProvider.GetRequiredService<IDeleteVenue>();

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
            
            var review = new Review(2.5M,
                                    3.5M,
                                    4.5M,
                                    1.5M,
                                    "dummy data",
                                    DateTime.Parse("01/01/2020"),
                                    venue
                                    );

            var review2 = new Review(2.5M,
                                    3.5M,
                                    4.5M,
                                    1.5M,
                                    "dummy data",
                                    DateTime.Parse("01/01/2020"),
                                    venue
                                    );

            _context.Add(venue);
            _context.Add(review);
            _context.Add(review2);

            _context.SaveChanges();
            
            // act & assert
            Assert.Throws(typeof(NotFoundException), () => venueDeleter.DeleteById(VENUE_ID_THAT_DOESNT_EXIST));

            Assert.IsTrue(_context.Set<Review>().Any(x => x.VenueId == VENUE_ID));
            Assert.IsTrue(_context.Set<Venue>().Any(x => x.VenueId == VENUE_ID));
        }
    }
}
