using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;

namespace Application.Venues
{
    internal class VenueCreator : ICreateVenue
    {
        private readonly IGenericRepository<Venue> _venueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VenueCreator(IGenericRepository<Venue> venueRepository,
                           IMapper mapper,
                           IUnitOfWork unitOfWork)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public VenueDto Create(CreateVenueDto createVenueDto)
        {
            var venue = new Venue(createVenueDto.Latitude,
                                  createVenueDto.Longitude,
                                  createVenueDto.Name,
                                  createVenueDto.VenueUrl,
                                  createVenueDto.ThumbnailUrl,
                                  createVenueDto.Phone,
                                  createVenueDto.TwitterHandle,
                                  createVenueDto.Address,
                                  createVenueDto.VenueType);

            _venueRepository.Insert(venue);

            _unitOfWork.SaveChanges();

            return _mapper.Map<VenueDto>(venue);
        }
    }
}