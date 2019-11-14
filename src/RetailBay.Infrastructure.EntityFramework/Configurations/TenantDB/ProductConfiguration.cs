using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.TenantDB
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.IsPublished)
                .IsRequired();
            
            builder.Property(p => p.ProductCategoryId)
                .IsRequired();
            
            builder.HasOne(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductCategoryId);
            
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
