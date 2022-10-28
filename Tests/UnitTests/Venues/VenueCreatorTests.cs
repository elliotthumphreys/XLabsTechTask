using Application.Venues;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Venues
{
    [TestFixture]
    internal class VenueCreatorTests : BaseTestFixture
    {
        [Test]
        public void Create_VenueCreated()
        {
            // arrange
            var createVenueDto = new CreateVenueDto
            {
                Longitude = 0,
                Latitude= 0,
                Name = "test venue",
                ThumbnailUrl = "https://exmaple.com",
                VenueUrl = "https://exmaple.com",
                VenueType = VenueType.Pub,
                Address = "test, test, test, TRTEST",
                TwitterHandle = "TEST Twitter Handle",
                Phone = "0738000000"
            };

            var venueCreator = _serviceProvider.GetRequiredService<ICreateVenue>();

            // act
            var createdVenue = venueCreator.Create(createVenueDto);

            // assert
            Assert.IsTrue(_context.Set<Venue>().Any(x => x.VenueId == createdVenue.VenueId));

            EnsureVenueMappedCorrectly(createVenueDto, createdVenue);
        }

        private void EnsureVenueMappedCorrectly(CreateVenueDto venue, VenueDto dto)
        {
            Assert.AreEqual(venue.Name, dto.Name);
            Assert.AreEqual(venue.Phone, dto.Phone);
            Assert.AreEqual(venue.Address, dto.Address);
            Assert.AreEqual(venue.Latitude, dto.Latitude);
            Assert.AreEqual(venue.Longitude, dto.Longitude);
            Assert.AreEqual(new Uri(venue.ThumbnailUrl).AbsoluteUri, dto.ThumbnailUrl);
            Assert.AreEqual(new Uri(venue.VenueUrl).AbsoluteUri, dto.VenueUrl);
            Assert.AreEqual(venue.TwitterHandle, dto.TwitterHandle);
            Assert.AreEqual(venue.VenueType.ToString(), dto.VenueType);
        }
    }
}
