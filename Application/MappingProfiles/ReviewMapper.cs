using AutoMapper;
using Domain;
using Dtos;

namespace Application.MappingProfiles
{
    public class ReviewMapper : Profile
    {
        public ReviewMapper()
        {
            CreateMap<Review, ReviewDto>()
            ;
        }
    }
}
