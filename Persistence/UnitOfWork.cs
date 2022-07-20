using System.Collections;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbcontext;
    private readonly Hashtable _repositories;

    public UnitOfWork(WordsDbContext dbcontext)
    {
        _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        _repositories = new Hashtable();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity);

        if (!_repositories.ContainsKey(type.Name))
        {
            var repositoryType = typeof(Repository<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type), _dbcontext);

            _repositories.Add(type.Name, repositoryInstance);
        }

        var repository = _repositories[type.Name] ?? throw new NullReferenceException("The repository you requested for wasn't created properly");

        return (IRepository<TEntity>)repository;
    }

    public async Task SaveChanges() => await _dbcontext.SaveChangesAsync();
}