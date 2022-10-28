using Dtos;

namespace Application.Venues
{
    public interface IUpdateVenueTag
    {
        ICollection<TagDto> Update(int venueId, ICollection<int> tagIds);
    }
}
