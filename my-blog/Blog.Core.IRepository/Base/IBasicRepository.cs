using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Model.interfaces;

namespace Blog.Core.IRepository.Base
{
    
    public interface IBasicRepository<TEntity> : IReadOnlyBasicRepository<TEntity>, IRepository
        where TEntity : class, IEntity
    {
        Task<TEntity> InsertAsync(
            TEntity entity,
            bool autoSave = false,
            CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(
            TEntity entity,
            bool autoSave = false,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
    }   
    public interface IBasicRepository<TEntity, TKey> : IBasicRepository<TEntity>, IReadOnlyBasicRepository<TEntity>, IRepository, IReadOnlyBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}