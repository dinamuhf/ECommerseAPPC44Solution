using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Dtos.ProductModule;


namespace ServiceAbstraction
{
    public interface IProductService
    {
    
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductQueryParams queryParams);
    
        Task<ProductResultDto?> GetProductByIdAsync(int Id);

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
     
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    }
}
