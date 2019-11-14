using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// CartRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.TenantDB.Cart}" />
    public interface ICartRepository : IRepository<Cart>
    {        
    }
}
