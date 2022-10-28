namespace Domain
{
    public class Review
    {
        private Review() { }

        public Review(decimal beerRating,
                      decimal atmosphereRating,
                      decimal amenitiesRating,
                      decimal valueForMoneyRating,
                      string  excerpt,
                      Venue venue) 
            : this(
                  beerRating,
                  atmosphereRating,
                  amenitiesRating,
                  valueForMoneyRating,
                  excerpt,
                  DateTime.UtcNow,
                  venue
                  ){}

        public Review(decimal beerRating,
                      decimal atmosphereRating,
                      decimal amenitiesRating,
                      decimal valueForMoneyRating,
                      string excerpt,
                      DateTime dateOfReview,
                      Venue venue) : this()
        {
            BeerRating = beerRating;
            AtmosphereRating = atmosphereRating;
            AmenitiesRating = amenitiesRating;
            ValueForMoneyRating = valueForMoneyRating;
            Excerpt = excerpt;

            DateOfReview = dateOfReview;

            VenueId = venue.VenueId;
            Venue = venue;

            venue.AddReview(this);
        }

        public int ReviewId { get; init; }

        public decimal BeerRating { get; init; }
        public decimal AtmosphereRating { get; init; }
        public decimal AmenitiesRating { get; init; }
        public decimal ValueForMoneyRating { get; init; }

        public DateTime DateOfReview { get; set; }

        public string Excerpt { get; init; } = default!;

        public int VenueId { get; init; }
        public Venue Venue { get; init; } = default!;
    }
}
