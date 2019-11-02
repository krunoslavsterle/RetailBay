using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();

            builder.HasOne(p => p.User)
                .WithOne(p => p.Cart)
                .HasForeignKey<Cart>(p => p.UserId);

            builder.HasOne(p => p.ShippingAddress)
                .WithMany(p => p.Carts)
                .HasForeignKey(p => p.ShippingAddressId);
        }
    }
}
