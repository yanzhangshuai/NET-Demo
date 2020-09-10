using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Common.Exceptions;
using Blog.Core.IRepository.Base;
using Blog.Core.IRepository.Base.EntityFramework;
using Blog.Core.Model.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Repository.Base.EntityFramework
{
  public class EfCoreRepository<TEntity, TKey> : EfCoreRepository<TEntity>, IEfCoreRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
  {
    public EfCoreRepository(IDbContextProvider dbContextProvider)
      : base(dbContextProvider)
    {
    }

    public virtual async Task<TEntity> GetAsync(TKey id,bool includeDetails = true,CancellationToken cancellationToken = default)
    {
      var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      return (object) entity != null ? entity : throw new EntityNotFoundException(typeof (TEntity), id);
    }

    public virtual async Task<TEntity> FindAsync(TKey id,bool includeDetails = true,CancellationToken cancellationToken = default)
    {
      TEntity entity;
      if (includeDetails)
        entity = await WithDetails()
          .FirstOrDefaultAsync(e => e.Id.Equals(id), GetCancellationToken(cancellationToken))
          .ConfigureAwait(false);
      else
        entity = await DbSet.FindAsync(new object[]
        {
          id
        }, GetCancellationToken(GetCancellationToken(cancellationToken)))
          .ConfigureAwait(false);
      return entity;
    }

    public virtual async Task DeleteAsync(TKey id,bool autoSave = false,CancellationToken cancellationToken = default)
    {
      var entity = await FindAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false);
      if (entity == null)
        return;
      await DeleteAsync(entity, autoSave, GetCancellationToken(cancellationToken)).ConfigureAwait(false);
    }
  }
}