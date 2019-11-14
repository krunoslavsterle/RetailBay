using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.SystemDB;

namespace RetailBay.Infrastructure.EntityFramework.Configurations.SystemDB
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenant");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.HostName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ConnectionString)
                .IsRequired();

            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
