using Dtos;

namespace Application.Venues
{
    public interface IReadVenue
    {
        VenueDto GetById(int venueId);
        ICollection<VenueDto> Get();
    }
}