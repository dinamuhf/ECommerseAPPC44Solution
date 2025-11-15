using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            //if (_Repositories.ContainsKey(TypeName))
            //{
            //    return (IGenericRepository<TEntity, TKey>)_Repositories[TypeName];

            //}
            if (_Repositories.TryGetValue(TypeName, out Object? Value))
            {
                return (IGenericRepository<TEntity, TKey>)Value;
            }
            else
            {
                var Repo = new GenericRepository<TEntity,TKey>(_dbContext);
                //  _Repositories.Add(TypeName, Repo);

                _Repositories[TypeName] = Repo;
                return Repo;

            }
        }

        public  async Task<int> saveChanges()
        
         => await _dbContext.SaveChangesAsync();
        
    }
}
