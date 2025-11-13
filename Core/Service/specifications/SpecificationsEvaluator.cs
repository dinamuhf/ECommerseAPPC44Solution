using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Service.specifications
{
    public static class SpecificationsEvaluator
    {
        public static  IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = inputQuery;
            if (specifications.Criteria != null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if(specifications.OrderBy != null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDescending != null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDescending);
            }
            if (specifications.IncludeExpression != null && specifications.IncludeExpression.Count>0)
            {
                Query = specifications.IncludeExpression.Aggregate(Query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }
            return Query;
        }
    }
}
