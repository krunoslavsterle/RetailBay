using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// OrderItemRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.TenantDB.OrderItem}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IOrderItemRepository" />
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrderItemRepository(TenantDBContext context) : base(context)
        {
        }

        #endregion Constructors
    }
}
