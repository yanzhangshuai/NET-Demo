using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Core.Model.interfaces;

namespace Blog.Core.IRepository.Base
{
    public interface IReadOnlyRepository<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IReadOnlyBasicRepository<TEntity>, IRepository
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> WithDetails();

        IQueryable<TEntity> WithDetails(
            params Expression<Func<TEntity, object>>[] propertySelectors);
    }
    
    public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IReadOnlyBasicRepository<TEntity>, IRepository, IReadOnlyBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
    }
}