using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework
{
    public class TenantDBContext : DbContext
    {
        private readonly string _connectionString;
        public TenantDBContext(Tenant tenant)
        {
            _connectionString = tenant.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString, options => options.MigrationsAssembly("RetailBay.Infrastructure.EntityFramework.Migrations"));

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories {get; set;}
        public DbSet<ProductPrice> ProductPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSnakeCaseNamingConvention(true);

            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<ProductPrice>(ConfigureProductPrice);
            builder.Entity<ProductCategory>(ConfigureProductCategory);
        }

        private void ConfigureProductCategory(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }

        private void ConfigureProductPrice(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();
            builder.OwnsOne(p => p.ProductPrice).HasForeignKey(p => p.Id);

            builder.HasOne(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
