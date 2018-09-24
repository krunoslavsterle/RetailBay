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
            builder.Entity<Product>(ConfigureProduct);
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.Property(ci => ci.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(ci => ci.Description)
                .HasColumnName("description")                
                .IsRequired(true);

            builder.Property(ci => ci.Price)
                .HasColumnName("price")
                .IsRequired(true);

            builder.Property(ci => ci.IsPublished)
                .HasColumnName("is_published")
                .IsRequired(true);

            builder.Property(ci => ci.DateCreated)
                .HasColumnName("date_created")                
                .IsRequired(true);

            builder.Property(ci => ci.DateUpdated)
                .HasColumnName("date_updated")
                .IsRequired(true);
        }
    }
}
