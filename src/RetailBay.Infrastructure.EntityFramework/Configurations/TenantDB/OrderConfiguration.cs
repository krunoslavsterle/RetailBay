using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.Property(p => p.ShippingAddressId)
                .IsRequired();

            builder.Property(p => p.OrderStatus)
                .IsRequired();

            builder.Property(p => p.OrderTotal)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.ShippingAddress)
                .WithMany()
                .HasForeignKey(p => p.ShippingAddressId);

            builder.HasMany(p => p.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);
            
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
