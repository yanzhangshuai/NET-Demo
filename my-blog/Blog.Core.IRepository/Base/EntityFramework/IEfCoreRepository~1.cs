using Blog.Core.Model.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.IRepository.Base.EntityFramework
{
    public interface IEfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        DbContext DbContext { get; }

        DbSet<TEntity> DbSet { get; }
    }
}