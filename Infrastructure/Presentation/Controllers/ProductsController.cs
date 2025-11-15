using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTOS;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProductsController(IServiceManager _serviceManager): ControllerBase
    {
        #region GetAllProducts
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllproductsAsync(queryParams);
            return Ok(Products);
        }
        #endregion

        #region GetProductById
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);

        }

        #endregion

        #region GetAllTypes
                [HttpGet("Types")]  
                public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
                {
                    var Types = await _serviceManager.ProductService.GetAllTypesAsync();
                    return Ok(Types);
        }
        #endregion
        #region GetAllBrands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        #endregion
    }
}
