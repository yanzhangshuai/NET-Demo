using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Common.Exceptions;
using Blog.Core.Model.interfaces;

namespace Blog.Core.IRepository.Base
{
    public abstract class BasicRepositoryBase<TEntity> : 
        IBasicRepository<TEntity>
        where TEntity : class, IEntity
    {
        public IServiceProvider ServiceProvider { get; set; }



        public abstract Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        public abstract Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        public abstract Task<IList<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);

        public abstract Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        protected virtual CancellationToken GetCancellationToken(CancellationToken preferredValue = default)
        {
              return !(preferredValue == new CancellationToken()) && !(preferredValue == CancellationToken.None) ? preferredValue : CancellationToken.None;
        }
    }
    
    public abstract class BasicRepositoryBase<TEntity, TKey> : BasicRepositoryBase<TEntity>, IBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public abstract Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
        
        public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave, cancellationToken);
        }
    }
    
}