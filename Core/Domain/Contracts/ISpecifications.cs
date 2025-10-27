using Domain.Entities.ProductModule;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Singature for property [Expression => where]
        public Expression<Func<TEntity,bool>>? Criteria { get; }
        // Singature for property [Expressions => Include] which will be more than 1 Include
        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }

        // Singature for OrderBy,OrderByDescending [Expression]
        public Expression<Func<TEntity,object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }
    }
}
