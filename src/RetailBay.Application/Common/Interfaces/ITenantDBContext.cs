using Microsoft.EntityFrameworkCore;
using RetailBay.Domain.Entities.Identity;
using RetailBay.Domain.Entities.TenantDB;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Common.Interfaces
{
    public interface ITenantDBContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Product> ProductPrices { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<UserAddress> UserAddresses { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
