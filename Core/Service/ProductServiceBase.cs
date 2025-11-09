using Shared.DTOS;

namespace Service
{
    public abstract class ProductServiceBase
    {
        public abstract  Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        public abstract Task<IEnumerable<ProductDto>> GetAllproductsAsync();
    }
}