using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Model.interfaces;

namespace Blog.Core.IRepository.Base
{
    public interface IReadOnlyBasicRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        Task<IList<TEntity>> GetListAsync(
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
    }
    
    public interface IReadOnlyBasicRepository<TEntity, TKey> : IReadOnlyBasicRepository<TEntity>
        where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}