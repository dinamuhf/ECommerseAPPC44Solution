using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.Dtos.ProductModule;
namespace Persentation.Controllers
{
    
    public class ProductsController(IServiceManager _serviceManager) : APIBaseController
    {
        #region Get All Products
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {

            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }
        #endregion
        #region GetProductById
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int Id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(Id);
            return Ok(Product);
        }

        #endregion
        #region Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        #endregion
        #region Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);

        }
        #endregion
    }
}

