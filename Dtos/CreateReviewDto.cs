namespace Dtos
{
    public class CreateReviewDto
    {
        public decimal BeerRating { get; set; }
        public decimal AtmosphereRating { get; set; }
        public decimal AmenitiesRating { get; set; }
        public decimal ValueForMoneyRating { get; set; }
        public string Excerpt { get; set; } = default!;
    }
}