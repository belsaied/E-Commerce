
using Domain.Entities.OrderModule;

namespace Presistence.Data
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
            // reach the Assembly which has the Configuration through this Black class [AssemblyReference] which brand the assembly of the project presistence.
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
