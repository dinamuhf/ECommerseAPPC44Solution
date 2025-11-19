using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using DomianLayer.Contracts;

namespace Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get ; set ; } 
        //Total Count =40
        //Page Size =10
       //Page Index = 2
       //10 ,10,10,10                        10             //2
        protected void ApplyPagination (int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;  //1*10 =10

        }
        #endregion

        protected void AddOrderBy(Expression<Func<TEntity, object>> oderByExp) => OrderBy = oderByExp;

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExp) => OrderByDescending = orderByDescendingExp;

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        
          =>  IncludeExpressions.Add(includeExpression);
        
    
    }
}
