using WordsHelper.Core.Models;

namespace WordsHelper.Core.Interfaces;

public interface IUnitOfWork<TEntity> where TEntity : BaseEntity
{
    IRepository<TEntity> GetRepository();
    Task<bool> SaveChanges();
}