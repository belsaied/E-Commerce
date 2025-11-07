global using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Presistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext , RoleManager<IdentityRole> _roleManager
        , UserManager<User> _userManager) : IDataSeeding
    {
        // ******should happen before the first request be executed
        public async Task SeedDataAsync()
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                // check if there is any Pending migrations--> Apply it.
                if (pendingMigrations.Any())
                {
                   await _dbContext.Database.MigrateAsync();
                }
                // check if the tables don't have data -> Seed (if they have data don't seed)
                // Consider seeding on childs which first 
                if (!_dbContext.ProductBrands.Any())
                {
                    // OpenRead -> return stream as DeserializeAsnc takes a stream not List.
                    var productBrandsData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    // json -> C# Object like [List<ProductBrands>]
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                }
                // do same for the productTypes.
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    // json -> C# Object like [List<ProductTypes>]
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (productTypes is not null && productTypes.Any())
                       await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                }
                // do the same for the product .
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    // json -> C# Object like [List<Product>]
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products is not null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle Exception
            }
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                // 1] Seed Roles [Admin , SuperAdmin]
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                // 2] Seed User [AdminUser , SuperAdminUser]
                if (!_userManager.Users.Any())
                {
                    var adminUser = new User()
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01008220807"
                    };
                    var superAdminUser = new User()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01550734422"
                    };
                    await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(superAdminUser, "Pa$$w0rd");

                    // 3] Assign Roles to Users (who do what)
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                }
            }
            catch ( Exception)
            {

                throw;
            }
        }
    }
}
