using Dtos;

namespace Application.Tags
{
    public interface IReadTag
    {
        TagDto GetById(int tagId);
        ICollection<TagDto> Get();
    }
}