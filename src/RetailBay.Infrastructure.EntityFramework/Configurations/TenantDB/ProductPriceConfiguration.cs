using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("product_price");

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.HasOne(p => p.Product)
                .WithOne(p => p.ProductPrice)
                .HasForeignKey<ProductPrice>(p => p.ProductId);
            
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
