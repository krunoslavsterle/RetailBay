using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.Identity;

namespace RetailBay.Infrastructure.EntityFramework.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();

            builder.HasOne(p => p.User)
                .WithMany(p => p.UserAddresses)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Address)
                .WithMany(p => p.UserAddresses)
                .HasForeignKey(p => p.AddressId);
        }
    }
}
