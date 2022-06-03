using Core.Models;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<TEntity> where TEntity : BaseEntity
{
    Expression<Func<TEntity, bool>> Criteria { get; set; }
    IList<Expression<Func<TEntity, object>>> Includes { get; set; }
    IList<string> IncludeStrings { get; set; }
    Expression<Func<TEntity, object>> OrderBy { get; set; }
    Expression<Func<TEntity, object>> OrderByDescending { get; set; }
    Expression<Func<TEntity, object>> GroupBy { get; set; }

    int Take { get; set; }
    int Skip { get; set; }
    bool IsPagingEnabled { get; set; }
}