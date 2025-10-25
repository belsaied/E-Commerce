using Domain.Entities.ProductModule;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        //GetAll
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        //GetById
        Task<TEntity?> GetByIdAsync(TKey id);
        //Add
        Task AddAsync(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Delete
        void Delete(TEntity entity);
        #region Specifications
        //GetAllWithSpecifications
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications);
        //GetByIdWithSpecifications
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        #endregion
    }
}
