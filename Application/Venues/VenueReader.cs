using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;

namespace Application.Venues
{
    internal class VenueReader : IReadVenue
    {
        private readonly IGenericRepository<Venue> _venueRepository;
        private readonly IMapper _mapper;

        public VenueReader(IGenericRepository<Venue> venueRepository,
                           IMapper mapper)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
        }

        public VenueDto GetById(int venueId)
        {
            var venue = _venueRepository.GetById(venueId);

            if(venue is null)
                throw new NotFoundException();

            return _mapper.Map<VenueDto>(venue);
        }

        public ICollection<VenueDto> Get()
        {
            var venues = _venueRepository.Get().ToList();

            return _mapper.Map<ICollection<VenueDto>>(venues);
        }
    }
}