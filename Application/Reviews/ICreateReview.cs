using Dtos;

namespace Application.Reviews
{
    public interface ICreateReview
    {
        ReviewDto Create(int venueId, CreateReviewDto createReviewDto);
    }
}