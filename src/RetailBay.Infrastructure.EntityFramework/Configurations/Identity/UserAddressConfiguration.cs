using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Domain.Entities.Identity;

namespace RetailBay.Infrastructure.EntityFramework.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("user_address");

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.AddressId)
                .IsRequired();

            builder.Property(p => p.AddressType)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.UserAddresses)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Address)
                .WithMany(p => p.UserAddresses)
                .HasForeignKey(p => p.AddressId);
            
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }
    }
}
