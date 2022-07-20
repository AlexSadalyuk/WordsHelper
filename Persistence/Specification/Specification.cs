using Core.Interfaces;
using Core.Models;
using System.Linq.Expressions;

namespace Persistence.Specification;

public class Specification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!;
    public IList<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
    public IList<string> IncludeStrings { get; set; } = new List<string>();
    public Expression<Func<TEntity, object>> OrderBy { get; set; } = null!;
    public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = null!;
    public Expression<Func<TEntity, object>> GroupBy { get; set; } = null!;
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPagingEnabled { get; set; }
}