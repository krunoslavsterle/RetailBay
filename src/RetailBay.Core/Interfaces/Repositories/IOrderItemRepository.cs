using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// OrderItemRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.TenantDB.OrderItem}" />
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
    }
}
