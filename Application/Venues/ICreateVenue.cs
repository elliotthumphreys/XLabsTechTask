using Dtos;

namespace Application.Venues
{
    public interface ICreateVenue
    {
        VenueDto Create(CreateVenueDto createVenueDto);
    }
}