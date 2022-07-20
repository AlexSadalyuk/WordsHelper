using Core.Models;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecificationBuilder<TEntity> where TEntity : BaseEntity
{
    ISpecificationBuilder<TEntity> AddCriteria(Expression<Func<TEntity, bool>> criteria);
    ISpecificationBuilder<TEntity> AddInclude(Expression<Func<TEntity, object>> include);
    ISpecificationBuilder<TEntity> AddIncludes(IEnumerable<Expression<Func<TEntity, object>>> includes);
    ISpecificationBuilder<TEntity> AddIncludeStrings(IEnumerable<string> strings);
    ISpecificationBuilder<TEntity> AddIncludeString(string strings);
    ISpecificationBuilder<TEntity> AddOrder(Expression<Func<TEntity, object>> order, bool ascending = true);
    ISpecificationBuilder<TEntity> AddGroupBy(Expression<Func<TEntity, object>> groupBy);
    ISpecificationBuilder<TEntity> AddPaging(int take, int skip = 0);
    ISpecification<TEntity> GetSpecification();
}