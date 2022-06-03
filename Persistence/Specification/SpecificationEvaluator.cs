using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Specification;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification) where TEntity : BaseEntity
    {
        if (query == null) throw new ArgumentNullException(nameof(query));
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.Includes != null)
        {
            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));
        }

        if (specification.IncludeStrings != null)
        {
            query = specification.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(entity => entity);
        }

        if (specification.IsPagingEnabled)
        {
            query = query
                .Skip(specification.Skip)
                .Take(specification.Take);
        }

        return query;
    }
}