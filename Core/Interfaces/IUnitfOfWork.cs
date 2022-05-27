using WordsHelper.Core.Models;

namespace WordsHelper.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task SaveChanges();
}