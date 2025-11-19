using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        //Get All Products With Types And Brands
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
           : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
           && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
           && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.Contains(queryParams.search.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
        }
        public ProductWithBrandAndTypeSpecifications(int id ):base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

    }
}
