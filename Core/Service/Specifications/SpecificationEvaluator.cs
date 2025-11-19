using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using DomianLayer.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public static  class SpecificationEvaluator
    {
        //Create Query?
        //_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);
    public static  IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity,Tkey> specifications) where TEntity:BaseEntity<Tkey>
        {
            //Step01
            var Query = InputQuery;
            // Step 02 : Check Critiera
            if(specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);

            }
            if (specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            }
            //Step 03 Check Includes
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count>0 )
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }
            if(specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    
    
    }
}
