using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductCountSpecifications(ProductQueryParams queryParams) :BaseSpecifications<Product,int>
        (P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
           && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
           && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.Contains(queryParams.search.ToLower())))
    {
    }
}
