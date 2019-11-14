using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.Identity;

namespace RetailBay.Infrastructure.EntityFramework.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");

            builder.Property(p => p.ContactName)
                .IsRequired();

            builder.Property(p => p.Phone)
                .IsRequired();

            builder.Property(p => p.StreetAddress)
                .IsRequired();

            builder.Property(p => p.PostalCode)
                .IsRequired();

            builder.Property(p => p.City)
                .IsRequired();

            builder.Property(p => p.Country)
                .IsRequired();

            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
