namespace Dtos
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int VenueId { get; set; }
        public decimal BeerRating { get; set; }
        public decimal AtmosphereRating { get; set; }
        public decimal AmenitiesRating { get; set; }
        public decimal ValueForMoneyRating { get; set; }
        public DateTime DateOfReview { get; set; }
        public string Excerpt { get; set; } = default!;
    }
}