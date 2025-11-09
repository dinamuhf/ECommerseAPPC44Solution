public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllproductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
}