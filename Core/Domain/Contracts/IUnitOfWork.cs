
using Domain.Entities.ProductModule;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        // Save.
        Task<int> SaveChangesAsync();
        // Method return Obj from Generic Repo of specific Entity.
        IGenericRepository<TEntity,TKey> GetReopsitory<TEntity,TKey>() where TEntity:BaseEntity<TKey>;
    }
}
