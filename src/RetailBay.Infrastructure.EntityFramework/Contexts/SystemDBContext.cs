using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.SystemDb;

namespace RetailBay.Infrastructure.EntityFramework
{
    public class SystemDBContext : DbContext
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSnakeCaseNamingConvention();
            builder.Entity<Tenant>(ConfigureTenant);
        }

        private void ConfigureTenant(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenant");

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true);

            builder.Property(ci => ci.HostName)
                .IsRequired(true);

            builder.Property(ci => ci.ConnectionString)
                .IsRequired(true);

            builder.Property(ci => ci.DateCreated)
                .IsRequired(true);

            builder.Property(ci => ci.DateUpdated)
                .IsRequired(true);
        }
    }
}
