using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application.Venues
{
    internal class VenueTagReader : IReadVenueTag
    {
        private readonly IGenericRepository<Venue> _venueRepository;
        private readonly IMapper _mapper;

        public VenueTagReader(IGenericRepository<Venue> venueRepository,
                              IMapper mapper)
        {
            _venueRepository=venueRepository;
            _mapper=mapper;
        }

        public ICollection<TagDto> GetById(int venueId)
        {
            var venue = _venueRepository.Get(x => x.VenueId == venueId)
                                        .Include(x => x.VenueTags)
                                            .ThenInclude(x => x.Tag)
                                        .SingleOrDefault();

            if(venue is null)
                throw new NotFoundException();

            return _mapper.Map<ICollection<TagDto>>(venue.Tags);
        }
    }
}
