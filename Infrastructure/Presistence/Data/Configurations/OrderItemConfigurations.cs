using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,4)");
            // to map ProductInOrderItem with its main class Order item.
            // It tells EF Core that the Product property inside your OrderItem entity is an owned entity type, not a separate entity with its own table.
            builder.OwnsOne(o => o.Product, p => p.WithOwner());
        }
    }
}
