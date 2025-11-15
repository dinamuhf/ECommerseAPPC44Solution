using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
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

        public async  Task<PaginatedResult<ProductDto>> GetAllproductsAsync(ProductQueryParams queryParams)
        {
            var Repo=_unitOfWork.GetRepository<Product,int>();
            var Specifications = new ProductWithBrandAndTypeSpecifications( queryParams);
            var AllProducts = await Repo.GetAllAsync( Specifications );
            var Data=_mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(AllProducts);
            var ProductCount= AllProducts.Count();
            var CountSpec=new ProductCountSpecifications(queryParams);
            var TotalCount= await Repo.CountAsync(CountSpec);

            return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductCount, TotalCount, Data);
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
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product, ProductDto>(product);

        }
    }
}
