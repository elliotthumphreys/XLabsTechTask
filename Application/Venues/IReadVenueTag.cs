using Dtos;

namespace Application.Venues
{
    public interface IReadVenueTag
    {
        ICollection<TagDto> GetById (int venueId);
    }
}
