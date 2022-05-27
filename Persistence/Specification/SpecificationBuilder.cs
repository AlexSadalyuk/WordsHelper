using System.Linq.Expressions;
using WordsHelper.Core.Interfaces;
using WordsHelper.Core.Models;

namespace WordsHelper.Persistence.Specification;

public class SpecificationBuilder<TEntity> : ISpecificationBuilder<TEntity> where TEntity : BaseEntity
{
    private ISpecification<TEntity> _specification;

    private SpecificationBuilder(ISpecification<TEntity> specification) 
    { 
        if(specification == null) throw new ArgumentNullException(nameof(specification));

        _specification = specification;
    }

    public static SpecificationBuilder<TEntity> Build<TSpecification>() 
        where TSpecification : ISpecification<TEntity>
    {
        var specificationType = typeof(TSpecification).MakeGenericType();

        var specificationInstance = Activator.CreateInstance(specificationType);

        if(specificationInstance is null) throw new NullReferenceException(nameof(specificationInstance));

        return new SpecificationBuilder<TEntity>((TSpecification)specificationInstance);
    }

    public ISpecificationBuilder<TEntity> AddCriteria(Expression<Func<TEntity, bool>> criteria)
    {
        if (criteria == null) throw new ArgumentNullException(nameof(criteria));

        _specification.Criteria = criteria;
        return this;
    }

    public ISpecificationBuilder<TEntity> AddInclude(Expression<Func<TEntity, object>> include)
    {
        if (include == null) throw new ArgumentNullException(nameof(include));

        _specification.Includes.Add(include);
        return this;
    }

    public ISpecificationBuilder<TEntity> AddIncludes(IEnumerable<Expression<Func<TEntity, object>>> includes)
    {
        if (includes == null) throw new ArgumentNullException(nameof(includes));
        if (includes.Any()) return this;

        _specification.Includes = includes.Aggregate(_specification.Includes, (prev, next) =>
        {
            prev.Add(next);
            return prev;
        });
        return this;
    }

    public ISpecificationBuilder<TEntity> AddGroupBy(Expression<Func<TEntity, object>> groupBy)
    {
        if (groupBy == null) throw new ArgumentNullException(nameof(groupBy));

        _specification.GroupBy = groupBy;
        return this;
    }

    public ISpecificationBuilder<TEntity> AddIncludeStrings(IEnumerable<string> strings)
    {
        if (strings == null) throw new ArgumentNullException(nameof(strings));
        if (strings.Any()) return this;

        _specification.IncludeStrings = strings.ToList();
        return this;
    }

    public ISpecificationBuilder<TEntity> AddIncludeString(string criteriaString)
    {
        if (string.IsNullOrWhiteSpace(criteriaString)) throw new ArgumentNullException(nameof(criteriaString));

        _specification.IncludeStrings.Add(criteriaString);
        return this;
    }

    public ISpecificationBuilder<TEntity> AddOrder(Expression<Func<TEntity, object>> order, bool ascending = true)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        if (ascending)
            _specification.OrderBy = order;
        else
            _specification.OrderByDescending = order;

        return this;
    }

    public ISpecificationBuilder<TEntity> AddPaging(int take, int skip = 0)
    {
        _specification.Skip = skip;
        _specification.Take = take;
        _specification.IsPagingEnabled = true;
        return this;
    }

    public ISpecification<TEntity> GetSpecification()
    {
        return _specification;
    }
}