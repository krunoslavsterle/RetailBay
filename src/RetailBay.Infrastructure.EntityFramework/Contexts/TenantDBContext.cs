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
            builder.UseSnakeCaseNamingConvention();

            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<ProductPrice>(ConfigureProductPrice);
            builder.Entity<ProductCategory>(ConfigureProductCategory);
        }

        private void ConfigureProductCategory(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("product_category");

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired();

            builder.Property(p => p.Id)                
                .IsRequired();

            builder.Property(p => p.Slug)
                .IsRequired();

            builder.Property(p => p.DateCreated)
                .IsRequired();

            builder.Property(p => p.DateUpdated)
                .IsRequired();
        }

        private void ConfigureProductPrice(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("product_price");

            builder.Property(p => p.Price)
                .IsRequired();
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Name)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(ci => ci.Description)
                .IsRequired(true);

            builder.Property(ci => ci.IsPublished)
                .IsRequired(true);

            builder.Property(ci => ci.DateCreated)
                .IsRequired(true);

            builder.Property(ci => ci.DateUpdated)
                .IsRequired(true);

            builder.Property(ci => ci.ProductCategoryId)
                .IsRequired(true);

            builder.OwnsOne(p => p.ProductPrice).HasForeignKey(p => p.Id);

            builder.HasOne(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
