using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_item");

            builder.Property(p => p.CartId)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.HasOne(p => p.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(p => p.CartId);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(p => p.ProductId);
            
            
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
