using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;

namespace Application.Reviews
{
    internal class ReviewsReader : IReadReview
    {
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewsReader(IGenericRepository<Review> venueRepository,
                           IMapper mapper)
        {
            _reviewRepository = venueRepository;
            _mapper = mapper;
        }

        public ReviewDto GetById(int venueId, int reviewId)
        {
            var review = _reviewRepository.Get(x => x.VenueId == venueId
                                                && x.ReviewId == reviewId)
                                          .SingleOrDefault();

            if(review is null)
                throw new NotFoundException();

            return _mapper.Map<ReviewDto>(review);
        }

        public ICollection<ReviewDto> Get(int venueId)
        {
            var reviews = _reviewRepository.Get(x => x.VenueId == venueId).ToList();

            return _mapper.Map<ICollection<ReviewDto>>(reviews);
        }
    }
}