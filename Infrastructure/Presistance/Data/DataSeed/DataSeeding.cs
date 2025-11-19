using DomainLayer.Contracts;
using DomainLayer.Models;
using DomianLayer.Models.IdentityModule;
using DomianLayer.Models.OrderModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;      



namespace Presistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : IDataSeeding
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
                var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(typesData);
                if (types != null && types.Any())
                {
                    await _dbContext.ProductTypes.AddRangeAsync(types);
                    await _dbContext.SaveChangesAsync();
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
            #region DeliveryMethod
            if (!_dbContext.DeliveryMethods.Any())
            {
                var DeliveryData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\deliveryjson");
                var Methods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryData);
                if (Methods != null && Methods.Any())
                {
                    await _dbContext.DeliveryMethods.AddRangeAsync(Methods);
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion

            #endregion
        }


        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));


                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        DisplayName = "Mohamed Ali",
                        Email = "Mohamed@gmail.com:",
                        PhoneNumber = "01012345678",
                        UserName = "MohamedAli",
                        EmailConfirmed = true

                    };
                    var User02 = new ApplicationUser
                    {
                        DisplayName = "salma Ali",
                        Email = "salam@gmail.com:",
                        PhoneNumber = "010122345678",
                        UserName = "salamAli",
                        EmailConfirmed = true

                    };
                    await _userManager.CreateAsync(User01, "Pa$$w0rd");
                    await _userManager.CreateAsync(User02, "Pa$$w0rd");
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
