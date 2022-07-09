using System.Linq.Expressions;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbContext _dbContext;

    private DbSet<TEntity> Entity => _dbContext.Set<TEntity>();

    protected GenericRepository(DbContext dbContext)
    {
        this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region Insert Methods

    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await Entity.AddAsync(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
    {
        await Entity.AddRangeAsync(entities);
        return await _dbContext.SaveChangesAsync();
    }

    public virtual int Add(TEntity entity)
    {
        Entity.Add(entity);
        return _dbContext.SaveChanges();
    }

    public virtual int Add(IEnumerable<TEntity> entities)
    {
        Entity.AddRange(entities);
        return _dbContext.SaveChanges();
    }

    #endregion

    #region Update Methods

    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        Entity.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;

        return await _dbContext.SaveChangesAsync();
    }

    public virtual int Update(TEntity entity)
    {
        Entity.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;

        return _dbContext.SaveChanges();
    }

    #endregion

    #region Delete Methods

    public virtual async Task<int> DeleteAsync(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            Entity.Attach(entity);
        }

        Entity.Remove(entity);

        return await _dbContext.SaveChangesAsync();
    }

    public virtual int Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            Entity.Attach(entity);
        }

        Entity.Remove(entity);

        return _dbContext.SaveChanges();
    }

    public virtual async Task<int> DeleteAsync(Guid id)
    {
        var entity = await Entity.FindAsync(id);
        return await DeleteAsync(entity);
    }

    public virtual int Delete(Guid id)
    {
        var entity = Entity.Find(id);
        return Delete(entity);
    }

    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        _dbContext.RemoveRange(Entity.Where(predicate));
        return _dbContext.SaveChanges() > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        _dbContext.RemoveRange(Entity.Where(predicate));
        return await _dbContext.SaveChangesAsync() > 0;
    }

    #endregion

    #region AddOrUpdate Methods

    public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
    {
        if (!Entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            _dbContext.Update(entity);

        return await _dbContext.SaveChangesAsync();
    }

    public virtual int AddOrUpdate(TEntity entity)
    {
        if (!Entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            _dbContext.Update(entity);

        return _dbContext.SaveChanges();
    }

    #endregion

    #region Get Methods

    public virtual IQueryable<TEntity> AsQueryable() => Entity.AsQueryable();

    public virtual Task<List<TEntity>> GetAll(bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Entity;

        if (predicate != null)
            query = query.Where(predicate);

        foreach (var include in includes)
            query = query.Include(include);

        if (orderBy != null)
            query = orderBy(query);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var found = await Entity.FindAsync(id);

        if (found == null)
            return null;

        if (noTracking)
            _dbContext.Entry(found).State = EntityState.Detached;

        foreach (var include in includes)
        {
            await _dbContext.Entry(found).Reference(include).LoadAsync();
        }

        return found;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Entity;

        if (predicate != null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync() ?? throw new InvalidOperationException();
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Entity.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return query;
    }

    #endregion

    #region Bulk Methods

    public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
    {
        if (ids != null && !ids.Any())
            return Task.CompletedTask;

        _dbContext.RemoveRange(Entity.Where(i => ids.Contains(i.Id)));

        return _dbContext.SaveChangesAsync();
    }

    public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task BulkDelete(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task BulkUpdate(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public async Task BulkAdd(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            await Task.CompletedTask;

        await Entity.AddRangeAsync(entities);

        await _dbContext.SaveChangesAsync();
    }

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

    public int SaveChanges() => _dbContext.SaveChanges();

    #endregion

    private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, includeItem) => current.Include(includeItem));
        }

        return query;
    }
}