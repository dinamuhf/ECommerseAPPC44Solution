using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var brandsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\brands.json");
                var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandsData);
                if (brands != null && brands.Any())
                {
                    await _dbContext.ProductBrands.AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion

            #region ProductTypes
            if (!_dbContext.ProductTypes.Any())
            {
                var typesData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\types.json");
                var types = await JsonSerializer.DeserializeAsync<List<ProductTybe>>(typesData);
                if (types != null && types.Any())
                {
                    await _dbContext.ProductTypes.AddRangeAsync(types);
                    await _dbContext.SaveChangesAsync(); // ✅ كمان هنا
                }
            }
            #endregion

            #region Products
            if (!_dbContext.Products.Any())
            {
                var productsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\products.json");
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                if (products != null && products.Any())
                {
                    await _dbContext.Products.AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync(); 
                }
            }
            #endregion

            #endregion
        }
    }
}
