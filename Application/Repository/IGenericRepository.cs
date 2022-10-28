using System.Linq.Expressions;

namespace Application.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T? GetById(params object[] ids);
        T Insert(T entity);
        T Delete(params object[] ids);
    }
}
