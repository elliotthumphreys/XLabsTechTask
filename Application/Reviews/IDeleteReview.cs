namespace Application.Reviews
{
    public interface IDeleteReview
    {
        void DeleteById(int venueId, int reviewId);
    }
}