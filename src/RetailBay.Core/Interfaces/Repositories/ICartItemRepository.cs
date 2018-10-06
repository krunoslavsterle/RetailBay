using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// CartItemRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.TenantDB.CartItem}" />
    public interface ICartItemRepository : IRepository<CartItem>
    {        
    }
}
