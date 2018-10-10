using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class ProductPriceConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();

            builder.HasOne(p => p.Product)
                .WithOne(p => p.ProductPrice)
                .HasForeignKey<Product>(p => p.ProductId);
        }
    }
}
