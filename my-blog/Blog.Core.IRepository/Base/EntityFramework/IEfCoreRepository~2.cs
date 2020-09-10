using Blog.Core.Model.interfaces;

namespace Blog.Core.IRepository.Base.EntityFramework
{
    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
    }
}