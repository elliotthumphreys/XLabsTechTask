using Application.Exceptions;
using Application.Reviews;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Reviews
{
    [TestFixture]
    internal class ReviewDeleterTests : BaseTestFixture
    {
        [Test]
        public void DeleteById_RemoveReviewForVenueThatDoesntExist_ThrowsNotFound()
        {
            // arrange
            const int REVIEW_ID = 1;
            const int VENUE_ID = 1;
            const int VENUE_ID_THAT_DOES_NOT_EXIST = 2;

            var reviewDeleter = _serviceProvider.GetRequiredService<IDeleteReview>();

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

            _context.Add(venue);
            _context.Add(review);

            _context.SaveChanges();
            
            // act & assert
            Assert.Throws(typeof(NotFoundException), () => reviewDeleter.DeleteById(VENUE_ID_THAT_DOES_NOT_EXIST, REVIEW_ID));

            Assert.IsTrue(_context.Set<Review>().Any(x => x.VenueId == VENUE_ID && x.ReviewId == REVIEW_ID));
        }

        [Test]
        public void DeleteById_RemoveReviewForVenueWithIncorrectVenueId_ThrowsNotFound()
        {
            // arrange
            const int REVIEW_ID = 1;
            const int VENUE_ID_THAT_WITH_REVIEW = 1;
            const int VENUE_ID_THAT_DOES_NOT_HAVE_REVIEW = 2;

            var reviewDeleter = _serviceProvider.GetRequiredService<IDeleteReview>();
            
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

            var venue2 = new Venue(1.1,
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

            _context.Add(venue);
            _context.Add(venue2);
            _context.Add(review);

            _context.SaveChanges();
            
            // act & assert
            Assert.Throws(typeof(NotFoundException), () => reviewDeleter.DeleteById(VENUE_ID_THAT_DOES_NOT_HAVE_REVIEW, REVIEW_ID));

            Assert.IsTrue(_context.Set<Review>().Any(x => x.VenueId == VENUE_ID_THAT_WITH_REVIEW && x.ReviewId == REVIEW_ID));
        }

        [Test]
        public void DeleteById_SuccessfullyRemovesReviewForVenue()
        {
            // arrange
            const int REVIEW_ID = 1;
            const int VENUE_ID = 1;

            var reviewDeleter = _serviceProvider.GetRequiredService<IDeleteReview>();

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

            _context.Add(venue);
            _context.Add(review);

            _context.SaveChanges();
            
            // act & assert
            Assert.DoesNotThrow(() => reviewDeleter.DeleteById(VENUE_ID, REVIEW_ID));

            Assert.IsFalse(_context.Set<Review>().Any(x => x.VenueId == VENUE_ID && x.ReviewId == REVIEW_ID));
        }
    }
}
