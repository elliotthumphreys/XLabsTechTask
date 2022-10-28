using Application.Exceptions;
using Application.Reviews;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Reviews
{
    [TestFixture]
    internal class ReviewReaderTests : BaseTestFixture
    {
        [Test]
        public void GetById_VenueHasSingleReview_ReturnsReviewDto()
        {
            // arrange
            const int REVIEW_ID = 1;
            const int VENUE_ID = 1;

            ReviewDto? reviewDto = null;

            var reviewReader = _serviceProvider.GetRequiredService<IReadReview>();

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
            Assert.DoesNotThrow(() => reviewDto = reviewReader.GetById(VENUE_ID, REVIEW_ID));
            Assert.IsNotNull(reviewDto);

            EnsureReviewMappedCorrectly(review, reviewDto!);
        }

        [Test]
        public void GetById_NoReviewForIds_NotFoundExceptionThrown()
        {
            // arrange
            const int REVIEW_ID = 1;
            const int VENUE_ID = 1;

            var reviewReader = _serviceProvider.GetRequiredService<IReadReview>();

            // act & assert
            Assert.Throws(typeof(NotFoundException), () => reviewReader.GetById(VENUE_ID, REVIEW_ID));
        }
        
        [Test]
        public void GetById_ReviewWithDifferentVenueId_NotFoundExceptionThrown()
        {
            // arrange
            const int REVIEW_ID = 1;
            const int VENUE_WITHOUT_REVIEW_ID = 2;
            
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

            var reviewReader = _serviceProvider.GetRequiredService<IReadReview>();

            // act & assert
            Assert.Throws(typeof(NotFoundException), () => reviewReader.GetById(VENUE_WITHOUT_REVIEW_ID, REVIEW_ID));
        }

        [Test]
        public void Get_ReadAllReviews()
        {
            // arrange
            const int VENUE_ID = 1;

            var reviewReader = _serviceProvider.GetRequiredService<IReadReview>();

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

            var review1 = new Review(2.5M,
                                    3.5M,
                                    4.5M,
                                    1.5M,
                                    "dummy data 1",
                                    DateTime.Parse("01/01/2020"),
                                    venue
                                    );
            
            var review2 = new Review(1.5M,
                                     2.5M,
                                     3.5M,
                                     4.5M,
                                     "dummy data 2",
                                     DateTime.Parse("02/01/2020"),
                                     venue
                                     );

            _context.Add(venue);
            _context.Add(review1);
            _context.Add(review2);

            _context.SaveChanges();

            // act
            var reviews = reviewReader.Get(VENUE_ID);

            // assert
            Assert.AreEqual(2, reviews.Count);

            EnsureReviewMappedCorrectly(review1, reviews.First());
            EnsureReviewMappedCorrectly(review2, reviews.Last());
        }

        private void EnsureReviewMappedCorrectly(Review review, ReviewDto dto)
        {
            Assert.AreEqual(review.VenueId, dto.VenueId);
            Assert.AreEqual(review.ReviewId, dto.ReviewId);
            Assert.AreEqual(review.DateOfReview, dto.DateOfReview);
            Assert.AreEqual(review.AmenitiesRating, dto.AmenitiesRating);
            Assert.AreEqual(review.BeerRating, dto.BeerRating);
            Assert.AreEqual(review.AtmosphereRating, dto.AtmosphereRating);
            Assert.AreEqual(review.ValueForMoneyRating, dto.ValueForMoneyRating);
            Assert.AreEqual(review.Excerpt, dto.Excerpt);
        }
    }
}
