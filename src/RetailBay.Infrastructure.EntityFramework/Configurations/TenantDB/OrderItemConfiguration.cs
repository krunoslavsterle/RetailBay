using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.Property(p => p.OrderId)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.ProductPrice)
                .IsRequired();

            builder.HasOne(p => p.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.ProductId);
            
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
