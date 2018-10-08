using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// OrderRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.TenantDB.Order}" />
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
