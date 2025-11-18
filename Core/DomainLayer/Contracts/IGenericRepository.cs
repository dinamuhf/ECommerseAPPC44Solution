using DomainLayer.Models;
using DomainLayer.Models.ProductModule;
using DomianLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        // GetAll
        Task<IEnumerable<TEntity>> GetAllAsync();
        // Get By Id
        Task<TEntity?> GetByIdAsync(Tkey id);
        #region With Specification
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications);
        #endregion
        // Add
        Task AddAsync(TEntity entity);
        // Update
        void Update(TEntity entity);
        // Delete
        void Remove(TEntity entity);
    }
}