global using Domain.Contracts;
using System.Text.Json;

namespace Presistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        // ******should happen before the first request be executed
        public void SeedData()
        {
            try
            {
                // check if there is any Pending migrations--> Apply it.
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                // check if the tables don't have data -> Seed (if they have data don't seed)
                // Consider seeding on childs which first 
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    // json -> C# Object like [List<ProductBrands>]
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                        _dbContext.ProductBrands.AddRange(productBrands);
                }
                // do same for the productTypes.
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    // json -> C# Object like [List<ProductTypes>]
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    if (productTypes is not null && productTypes.Any())
                        _dbContext.ProductTypes.AddRange(productTypes);
                }
                // do the same for the product .
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    // json -> C# Object like [List<Product>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null && products.Any())
                        _dbContext.Products.AddRange(products);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle Exception
            }
        }
    }
}
