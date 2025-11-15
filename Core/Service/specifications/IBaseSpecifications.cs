using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using System.Linq.Expressions;

namespace Service.specifications
{
    public interface IBaseSpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>, ISpecifications<TEntity, TKey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> IncludeExpression { get; }
    }
}