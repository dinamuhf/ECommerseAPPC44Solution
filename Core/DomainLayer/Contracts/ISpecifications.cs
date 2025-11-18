using DomainLayer.Models;
using DomainLayer.Models.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayer.Contracts
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        //_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        #region Sorting
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        #endregion
        #region Pagination
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; set; }

        #endregion

    }
}
