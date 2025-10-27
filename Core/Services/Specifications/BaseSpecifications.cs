using Domain.Contracts;
using Domain.Entities.ProductModule;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey>
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria [where]
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpressions)
            => IncludeExpressions.Add(includeExpressions);
        #endregion

        #region Sorting [OrderBy , OrderByDesc]
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) => OrderByDescending = orderByDescendingExpression;
        #endregion
    }
}
