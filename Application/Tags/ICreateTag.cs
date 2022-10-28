using Dtos;

namespace Application.Tags
{
    public interface ICreateTag
    {
        TagDto Create(CreateTagDto createTagDto);
    }
}