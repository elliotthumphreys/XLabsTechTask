using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;

namespace Application.Reviews
{
    internal class ReviewCreator : ICreateReview
    {
        private readonly IGenericRepository<Venue> _venueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewCreator(IGenericRepository<Venue> venueRepository,
                            IMapper mapper,
                            IUnitOfWork unitOfWork)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public ReviewDto Create(int venueId, CreateReviewDto createReviewDto)
        {
            var venue = _venueRepository.GetById(venueId);

            if (venue == null)
                throw new NotFoundException();
            
            var review = new Review(createReviewDto.BeerRating,
                                    createReviewDto.AtmosphereRating,
                                    createReviewDto.AmenitiesRating,
                                    createReviewDto.ValueForMoneyRating,
                                    createReviewDto.Excerpt,
                                    venue);

            _unitOfWork.SaveChanges();

            return _mapper.Map<ReviewDto>(review);
        }
    }
}