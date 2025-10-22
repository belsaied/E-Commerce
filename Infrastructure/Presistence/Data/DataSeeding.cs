global using Domain.Contracts;
using System.Text.Json;

namespace Presistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
    }
}
