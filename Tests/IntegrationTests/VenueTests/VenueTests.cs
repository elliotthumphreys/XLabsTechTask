using Domain;
using Dtos;
using IntegrationTests.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Text;

namespace IntegrationTests.VenueTests
{
    [TestFixture]
    internal class VenueTests : BaseTestFixture
    {
        private int? _lastVenueId = null;
        
        [OneTimeSetUp]
        public void SetUp()
        {
            _lastVenueId = _context.Set<Venue>().OrderByDescending(x => x.VenueId).FirstOrDefault()?.VenueId ?? 0;
        }

        [TearDown]
        public void TearDown()
        {
            var venuesCreatedInThisTestFixture = _context.Set<Venue>()
                                                         .Where(x => x.VenueId > _lastVenueId)
                                                         .ToList();

            _context.RemoveRange(venuesCreatedInThisTestFixture);

            _context.SaveChanges();
        }

        [Test]
        public async Task Get_ReturnsAllVenues()
        {
            // arrange
            var venueCountInDb = _context.Set<Venue>().Count();

            // act
            var venuesResponse = await _httpClient.GetAsync("venues");

            // assert
            Assert.True(venuesResponse.IsSuccessStatusCode);

            var venues = await venuesResponse.Content.ReadFromJsonAsync<ICollection<VenueDto>>();

            Assert.NotNull(venues);
            Assert.AreEqual(venueCountInDb, venues!.Count);
        }

        [Test]
        public async Task Create_CanCreateProduct()
        {
            // arrange
            var createVenueDto = new CreateVenueDto
            {
                Longitude = 1,
                Latitude = 2,
                Name = "Venue test name",
                ThumbnailUrl = "https://exmaple.com",
                VenueUrl = "https://exmaple.com",
                VenueType = VenueType.Pub,
                Address = "test, test, test, TRTEST",
                TwitterHandle = "TEST Twitter Handle",
                Phone = "0738000000"
            };
            
            var json = JsonConvert.SerializeObject(createVenueDto);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");


            // act
            var venueResponse = await _httpClient.PostAsync("venues", postData);

            // assert
            Assert.True(venueResponse.IsSuccessStatusCode);
            
            var venue = await venueResponse.Content.ReadFromJsonAsync<VenueDto>();
            
            Assert.NotNull(venue);

            var venueCreatedInDb = _context.Set<Venue>().SingleOrDefault(x => x.VenueId == venue!.VenueId);
            
            Assert.NotNull(venueCreatedInDb);

            EnsureVenueMappedCorrectly(createVenueDto, venue!);
        }


        [Test]
        public async Task Create_CanDeleteProduct()
        {
            // arrange
            var createVenueDto = new CreateVenueDto
            {
                Longitude = 1,
                Latitude = 2,
                Name = "Venue test name",
                ThumbnailUrl = "https://exmaple.com",
                VenueUrl = "https://exmaple.com",
                VenueType = VenueType.Pub,
                Address = "test, test, test, TRTEST",
                TwitterHandle = "TEST Twitter Handle",
                Phone = "0738000000"
            };

            var json = JsonConvert.SerializeObject(createVenueDto);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var venueResponse = await _httpClient.PostAsync("venues", postData);
            Assert.True(venueResponse.IsSuccessStatusCode);

            var venue = await venueResponse.Content.ReadFromJsonAsync<VenueDto>();
            Assert.NotNull(venue);

            // act
            var response = await _httpClient.DeleteAsync($"venues/{venue!.VenueId}");

            // assert
            Assert.True(venueResponse.IsSuccessStatusCode);

            var venueThatShouldBeRemoved = _context.Set<Venue>().SingleOrDefault(x => x.VenueId == venue!.VenueId);

            Assert.IsNull(venueThatShouldBeRemoved);
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
