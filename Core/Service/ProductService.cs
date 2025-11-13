using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.specifications;
using ServiceAbstraction;
using Shared;
using Shared.DTOS;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
      public  async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
           var Repo= _unitOfWork.GetRepository<ProductBrand,int>();
            var Brands= await Repo.GetAllAsync();
            var BrandsDto= _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
            return BrandsDto;

        }

        public async  Task<IEnumerable<ProductDto>> GetAllproductsAsync(ProductQueryParams queryParams)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications( queryParams);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Specifications);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
        }

        public async  Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
        var Types= await _unitOfWork.GetRepository<ProductTybe,int>().GetAllAsync();
            return  _mapper.Map<IEnumerable<ProductTybe>,IEnumerable<TypeDto>>(Types);
        }

    public async   Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product= await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(Specifications);
            return _mapper.Map<Product, ProductDto>(product);

        }
    }
}
