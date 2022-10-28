using Dtos;

namespace Application.Reviews
{
    public interface IReadReview
    {
        ReviewDto GetById(int venueId, int reviewId);
        ICollection<ReviewDto> Get(int venueId);
    }
}