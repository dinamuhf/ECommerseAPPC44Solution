using Shared;
using Shared.DTOS;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
      Task < PaginatedResult<ProductDto>>GetAllproductsAsync(ProductQueryParams queryParams);
        Task<ProductDto?> GetProductByIdAsync( int id);
        Task <IEnumerable<TypeDto>> GetAllTypesAsync();
        Task <IEnumerable<BrandDto>> GetAllBrandsAsync();
      
        
    }
}
