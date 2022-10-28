using Domain;

namespace Dtos
{
    public class CreateVenueDto
    {
        public string Name { get; set; } = default!;
        public string VenueUrl { get; set; } = default!;
        public string ThumbnailUrl { get; set; } = default!;
        public string? Phone { get; set; } 
        public string? TwitterHandle { get; set; } 
        public string? Address { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public VenueType VenueType { get; set; }
    }
}