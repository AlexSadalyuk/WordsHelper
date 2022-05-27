using System.Linq.Expressions;
using WordsHelper.Core.Interfaces;
using WordsHelper.Core.Models;

namespace WordsHelper.Persistence.Specification;

public class Specification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!;
    public IList<Expression<Func<TEntity, object>>> Includes { get; set; } = null!;
    public IList<string> IncludeStrings { get; set; } = null!;
    public Expression<Func<TEntity, object>> OrderBy { get; set; } = null!;
    public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = null!;
    public Expression<Func<TEntity, object>> GroupBy { get; set; } = null!;
    public int Take { get; set; } 
    public int Skip { get; set; }
    public bool IsPagingEnabled { get; set; }
}