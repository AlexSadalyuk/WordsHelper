using WordsHelper.Core.Models;

namespace WordsHelper.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> Get(ISpecification<TEntity>? spec = null);
    Task<TEntity> Get(int id);
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> AddRange(IEnumerable<TEntity> entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Delete(int id);
    Task<TEntity> Delete(IEnumerable<int> ids);
    Task<TEntity> Delete(TEntity entity);
    Task<TEntity> Delete(IEnumerable<TEntity> entity);
}