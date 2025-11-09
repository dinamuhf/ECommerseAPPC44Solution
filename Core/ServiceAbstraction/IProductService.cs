using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
      Task < IEnumerable<ProductDto>>GetAllproductsAsync();
        Task<ProductDto?> GetProductByIdAsync( int id);
        Task <IEnumerable<TypeDto>> GetAllTypesAsync();
        Task <IEnumerable<BrandDto>> GetAllBrandsAsync();
      
        
    }
}
