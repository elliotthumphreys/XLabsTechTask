using AutoMapper;
using Domain;
using Dtos;

namespace Application.MappingProfiles
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {    
            CreateMap<Tag, TagDto>()
            ;
        }
    }
}
