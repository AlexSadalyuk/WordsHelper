using Core.Models;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task SaveChanges();
}