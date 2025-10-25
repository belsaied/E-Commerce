﻿using Domain.Contracts;
using Domain.Entities.ProductModule;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey>
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpressions)
            => IncludeExpressions.Add(includeExpressions);
    }
}
