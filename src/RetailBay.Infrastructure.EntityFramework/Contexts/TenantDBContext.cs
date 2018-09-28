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

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSnakeCaseNamingConvention();
            builder.Entity<Product>(ConfigureProduct);
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

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.Property(ci => ci.IsPublished)
                .IsRequired(true);

            builder.Property(ci => ci.DateCreated)
                .IsRequired(true);

            builder.Property(ci => ci.DateUpdated)
                .IsRequired(true);
        }
    }
}
