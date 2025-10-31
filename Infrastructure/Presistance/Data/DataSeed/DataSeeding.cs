using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                               _dbContext.Database.Migrate();
            }
            #region Seeding Data
            #region ProductBrands
            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\brands.json");
                var Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                if (Brands != null && Brands.Any())
                {
                   
                   await     _dbContext.ProductBrands.AddRangeAsync(Brands);
                  
                }

            }
            #endregion

            #region ProductTypes
            if (!_dbContext.ProductTypes.Any())
            {
                var TypesData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\types.json");
                var Types = await JsonSerializer.DeserializeAsync<List<ProductTybe>>(TypesData);
                if (Types != null && Types.Any())
                {
                await    _dbContext.ProductTypes.AddRangeAsync(Types);
                }
            }
            #endregion

            #region Products
            if (!_dbContext.Products.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if (Products != null && Products.Any())
                {
                   
                   await     _dbContext.Products.AddRangeAsync(Products);
                  
                }
            }
            #endregion

           

            #endregion
        }
    }
}