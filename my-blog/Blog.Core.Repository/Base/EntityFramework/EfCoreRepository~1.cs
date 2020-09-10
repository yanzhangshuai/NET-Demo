using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Blog.Core.Common;
using Blog.Core.IRepository.Base;
using Blog.Core.IRepository.Base.EntityFramework;
using Blog.Core.Model.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Repository.Base.EntityFramework
{
   public class EfCoreRepository<TEntity> : RepositoryBase<TEntity>, IEfCoreRepository<TEntity> 
    where TEntity : class, IEntity
  {
    private readonly IDbContextProvider _dbContextProvider;
    public DbContext DbContext => _dbContextProvider.GetDbContext();
    public virtual DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    public EfCoreRepository(IDbContextProvider dbContextProvider)
    {
      _dbContextProvider = dbContextProvider;
    }

    public override async Task<TEntity> InsertAsync(TEntity entity,bool autoSave = false,CancellationToken cancellationToken = default)
    {
      var savedEntity = (await DbSet.AddAsync(entity, cancellationToken)).Entity;
      if (autoSave)
      {
        await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      }
      return savedEntity;
    }

    public override async Task<TEntity> UpdateAsync(TEntity entity,bool autoSave = false,CancellationToken cancellationToken = default)
    {
      DbContext.Attach(entity);
      var updatedEntity = DbContext.Update(entity).Entity;
      if (autoSave)
      {
        await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      }
      return updatedEntity;
    }

    public override async Task DeleteAsync(TEntity entity,bool autoSave = false,CancellationToken cancellationToken = default)
    {
      DbSet.Remove(entity);
      if (!autoSave)
        return;
      await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
    }

    public override async Task<IList<TEntity>> GetListAsync(bool includeDetails = false,CancellationToken cancellationToken = default)
    {
      List<TEntity> entityList;
      if (includeDetails)
        entityList = await WithDetails().ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      else
        entityList = await DbSet.ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      return entityList;
    }

    public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
      return await DbSet.LongCountAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
    }

    protected override IQueryable<TEntity> GetQueryable() => DbSet.AsQueryable();

    public override async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,bool includeDetails = true,CancellationToken cancellationToken = default)
    {
      TEntity entity;
      if (includeDetails)
        entity = await WithDetails().Where(predicate).SingleOrDefaultAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      else
        entity = await DbSet.Where(predicate).SingleOrDefaultAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
      return entity;
    }

    public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,bool autoSave = false,CancellationToken cancellationToken = default)
    {
      foreach (var entity in await GetQueryable().Where(predicate).ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false))
        DbSet.Remove(entity);
      if (!autoSave)
        return;
      await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
    }

    public virtual async Task EnsureCollectionLoadedAsync<TProperty>(TEntity entity,Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,CancellationToken cancellationToken = default)
      where TProperty : class
    {
      await DbContext.Entry(entity).Collection(propertyExpression).LoadAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
    }

    public virtual async Task EnsurePropertyLoadedAsync<TProperty>(TEntity entity,Expression<Func<TEntity, TProperty>> propertyExpression,CancellationToken cancellationToken = default)
      where TProperty : class
    {
      await DbContext.Entry(entity).Reference(propertyExpression).LoadAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
    }

    public override IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
      var source = GetQueryable();
      return propertySelectors.IsNullOrEmpty() ? 
        source : propertySelectors.
          Aggregate(source, (current, propertySelector) => current.Include(propertySelector));
    }
  }
}