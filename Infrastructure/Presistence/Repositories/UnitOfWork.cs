using Presistence.Data;
using System.Collections.Concurrent;

namespace Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }
        public IGenericRepository<TEntity, TKey> GetReopsitory<TEntity, TKey>() where TEntity : BaseEntity<TKey>
           => (IGenericRepository<TEntity, TKey>)_repositories
            .GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_dbContext));
            #region using Normal Dictionary.
            //var key = typeof(TEntity).Name;      // Name of the Entity as a string.
            //if (!_repositories.ContainsKey(key))
            //    _repositories[key] = new GenericRepository<TEntity, TKey>(_dbContext);
            //return (IGenericRepository<TEntity, TKey>)_repositories[key]; 
            #endregion
        

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
