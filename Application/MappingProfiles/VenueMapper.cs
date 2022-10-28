using AutoMapper;
using Domain;
using Dtos;

namespace Application.MappingProfiles
{
    public class VenueMapper : Profile
    {
        public VenueMapper()
        {    
            CreateMap<Venue, VenueDto>()
                .ForMember(dto => dto.VenueUrl, dom => dom.MapFrom(m => m.VenueUrl.AbsoluteUri))
                .ForMember(dto => dto.ThumbnailUrl, dom => dom.MapFrom(m => m.ThumbnailUrl.AbsoluteUri))
                .ForMember(dto => dto.VenueType, dom => dom.MapFrom(m => m.VenueType.ToString()))
            ;
        }
    }
}
