using Application.Exceptions;
using Application.Repository;
using Domain;

namespace Application.Reviews
{
    internal class ReviewDeleter : IDeleteReview
    {
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewDeleter(IGenericRepository<Review> reviewRepository,
                             IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork=unitOfWork;
        }

        public void DeleteById(int venueId, int reviewId)
        {
            var review = _reviewRepository.Get(x => x.VenueId == venueId
                                                && x.ReviewId == reviewId)
                                          .SingleOrDefault();

            if(review is null)
                throw new NotFoundException();

            _reviewRepository.Delete(review.ReviewId);

            _unitOfWork.SaveChanges();
        }
    }
}