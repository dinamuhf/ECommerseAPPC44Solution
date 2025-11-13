using DomainLayer.Models;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.specifications
{
    public class ProductWithBrandAndTypeSpecifications: BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base
            (p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) &&
            (!queryParams.TypeId.HasValue || p.TybeId == queryParams.TypeId) &&
            (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.Contains(queryParams.SearchValue.ToLower())
            ))
           
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductTybe);
            #region Sorting
            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:AddOrderBy(P=>P.Name);
                    break;
                    case ProductSortingOptions.NameDesc:AddOrderByDescending(P=>P.Name);
                    break;
                    case ProductSortingOptions.PriceAsc:AddOrderBy(P=>P.Price);
                    break;
                    case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P=>P.Price);
                    break;
                    default:
                    break;
            }

            #endregion
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductTybe);
        }
    }
}
