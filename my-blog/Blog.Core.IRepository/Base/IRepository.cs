using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Model.interfaces;

namespace Blog.Core.IRepository.Base
{
    public interface IRepository
    {
    }
    
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IReadOnlyBasicRepository<TEntity>, IRepository, IBasicRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool autoSave = false,
            CancellationToken cancellationToken = default);
    }
    
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IReadOnlyRepository<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IReadOnlyBasicRepository<TEntity>, IRepository, IBasicRepository<TEntity>, IReadOnlyRepository<TEntity, TKey>, IReadOnlyBasicRepository<TEntity, TKey>, IBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
    }
}