using System.Collections;
using Microsoft.EntityFrameworkCore;
using WordsHelper.Core.Interfaces;
using WordsHelper.Core.Models;

namespace WordsHelper.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbcontext;
    private readonly Hashtable _repositories;

    public UnitOfWork(DbContext dbcontext)
    {
        _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        _repositories = new Hashtable();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity);

        if(!_repositories.ContainsKey(type.Name))
        {
            var repositoryType = typeof(Repository<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type), _dbcontext);

            _repositories.Add(type, repositoryInstance);
        }

        var repository = _repositories[type.Name] ?? throw new NullReferenceException("The repository you requested for wasn't created properly");

        return (IRepository<TEntity>) repository;
    }

    public async Task SaveChanges() => await _dbcontext.SaveChangesAsync();
}