using Core.Models;

namespace Core.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyCollection<TEntity>> Get(ISpecification<TEntity>? spec = null);
    Task<TEntity> Get(int id);
    Task<TEntity> Add(TEntity entity);
    Task AddRange(IEnumerable<TEntity> entity);
    TEntity Update(TEntity entity);
    Task<TEntity> Delete(int id);
    Task Delete(IEnumerable<int> ids);
    Task<TEntity> Delete(TEntity entity);
    Task Delete(IEnumerable<TEntity> entity);
}