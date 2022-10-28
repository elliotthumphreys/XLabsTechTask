using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            if (_context == null)
            {
                throw new ArgumentNullException("context");
            }

            _dbSet = _context.Set<T>();
        }

        public T Delete(params object[] ids)
        {
            var val = _dbSet.Find(ids);
            if (val == null)
            {
                throw new Exception("Entity to delete was not found");
            }

            return _dbSet.Remove(val).Entity;
        }

        public IQueryable<T> Get()
        {
            return Get(null);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? predicate)
        {
            IQueryable<T> queryable = _dbSet;
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            return queryable;
        }

        public T? GetById(params object[] ids)
        {
            return _dbSet.Find(ids);
        }

        public T Insert(T entity)
        {
            var entityEntry = _dbSet.Add(entity);
            
            return entityEntry.Entity;
        }
    }
}
