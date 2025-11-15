using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.specifications
{
    public class ProductCountSpecifications(ProductQueryParams queryParams):BaseSpecifications<Product,int>(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) &&
            (!queryParams.TypeId.HasValue || p.TybeId == queryParams.TypeId) &&
            (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.Contains(queryParams.SearchValue.ToLower())
            ))
    {
    }
}
