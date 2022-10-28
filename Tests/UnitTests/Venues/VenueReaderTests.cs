using Application.Exceptions;
using Application.Venues;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Venues
{
    [TestFixture]
    internal class VenueReaderTests : BaseTestFixture
    {
        [Test]
        public void GetById_HasSingleVenue_ReturnsVenueDto()
        {
            // arrange
            const int VENUE_ID = 1;

            VenueDto? venueDto = null;

            var venueReader = _serviceProvider.GetRequiredService<IReadVenue>();

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
            
           
            // act & assert
            Assert.DoesNotThrow(() => venueDto = venueReader.GetById(VENUE_ID));
            Assert.IsNotNull(venueDto);

            EnsureVenueMappedCorrectly(venue, venueDto!);
        }

        [Test]
        public void GetById_NoVenueForId_NotFoundExceptionThrown()
        {
            // arrange
            var venueReader = _serviceProvider.GetRequiredService<IReadVenue>();

            // act & assert
            Assert.Throws(typeof(NotFoundException), () => venueReader.GetById(1));
        }

        [Test]
        public void Get_ReadAllVenues()
        {
            // arrange
            var venueReader = _serviceProvider.GetRequiredService<IReadVenue>();
            
            var venue1 = new Venue(0,
                                   0,
                                   "test venue",
                                   "https://exmaple.com",
                                   "https://exmaple.com",
                                   null,
                                   null,
                                   null,
                                   VenueType.Pub,
                                   Array.Empty<Tag>());

            var venue2 = new Venue(0,
                                   0,
                                   "test venue 2",
                                   "https://exmaple.com",
                                   "https://exmaple.com",
                                   null,
                                   null,
                                   null,
                                   VenueType.Bar,
                                   Array.Empty<Tag>());

            _context.Add(venue1);
            _context.Add(venue2);

            _context.SaveChanges();

            // act
            var venues = venueReader.Get();

            // assert
            Assert.AreEqual(2, venues.Count);

            EnsureVenueMappedCorrectly(venue1, venues.First());
            EnsureVenueMappedCorrectly(venue2, venues.Last());
        }

        private void EnsureVenueMappedCorrectly(Venue venue, VenueDto dto)
        {
            Assert.AreEqual(venue.Name, dto.Name);
            Assert.AreEqual(venue.Phone, dto.Phone);
            Assert.AreEqual(venue.Address, dto.Address);
            Assert.AreEqual(venue.Latitude, dto.Latitude);
            Assert.AreEqual(venue.Longitude, dto.Longitude);
            Assert.AreEqual(venue.ThumbnailUrl.AbsoluteUri, dto.ThumbnailUrl);
            Assert.AreEqual(venue.VenueUrl.AbsoluteUri, dto.VenueUrl);
            Assert.AreEqual(venue.TwitterHandle, dto.TwitterHandle);
            Assert.AreEqual(venue.VenueType.ToString(), dto.VenueType);
        }
    }
}
