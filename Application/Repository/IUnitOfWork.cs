namespace Application.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}