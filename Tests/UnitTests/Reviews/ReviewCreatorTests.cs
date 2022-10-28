using Application.Reviews;
using Application.Venues;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Venues
{
    [TestFixture]
    internal class ReviewCreatorTests : BaseTestFixture
    {
        [Test]
        public void Create_ReviewCreated()
        {
            // arrange
            const int VENUE_ID = 1;
            const int REVIEW_ID = 1;

            var approxDateTimeCreated = DateTime.UtcNow;
            
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

            _context.SaveChanges();

            var createReviewDto = new CreateReviewDto
            {
                AmenitiesRating = 2.1M,
                AtmosphereRating = 2.4M,
                BeerRating = 2.5M,
                ValueForMoneyRating = 5.0M,
                Excerpt = "Test excerpt"
            };

            var reviewCreator = _serviceProvider.GetRequiredService<ICreateReview>();

            // act
            var createdReview = reviewCreator.Create(VENUE_ID, createReviewDto);

            // assert
            Assert.IsTrue(_context.Set<Review>().Any(x => x.VenueId == VENUE_ID
                                                          && x.ReviewId == REVIEW_ID));

            EnsureVenueMappedCorrectly(createReviewDto, createdReview);

            Assert.IsTrue(approxDateTimeCreated <= createdReview.DateOfReview);
        }

        private void EnsureVenueMappedCorrectly(CreateReviewDto review, ReviewDto dto)
        {
            Assert.AreEqual(review.BeerRating, dto.BeerRating);
            Assert.AreEqual(review.AmenitiesRating, dto.AmenitiesRating);
            Assert.AreEqual(review.AtmosphereRating, dto.AtmosphereRating);
            Assert.AreEqual(review.ValueForMoneyRating, dto.ValueForMoneyRating);
            Assert.AreEqual(review.Excerpt, dto.Excerpt);
        }
    }
}
