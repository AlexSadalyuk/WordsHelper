using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Specification;

namespace Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbContext DbContext;
    public Repository(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IReadOnlyCollection<TEntity>> Get(ISpecification<TEntity> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        var query = DbContext.Set<TEntity>().AsQueryable<TEntity>();
        var result = await SpecificationEvaluator.ApplySpecification(query, specification).ToListAsync();
        return result.AsReadOnly();
    }

    public async Task<TEntity> Get(int id)
    {
        return await DbContext
            .Set<TEntity>()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var newEntity = await DbContext.Set<TEntity>().AddAsync(entity);
        return newEntity.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var updatedEntity = DbContext.Set<TEntity>().Update(entity);
        return updatedEntity.Entity;
    }

    public async Task<TEntity> Delete(int id)
    {
        var toRemove = await Get(id);

        if (toRemove == null) return null;

        var removedEntity = DbContext.Set<TEntity>().Remove(toRemove);
        return removedEntity.Entity;
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await DbContext.AddRangeAsync(entities);
    }

    public Task Delete(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(IEnumerable<TEntity> entity)
    {
        throw new NotImplementedException();
    }
}