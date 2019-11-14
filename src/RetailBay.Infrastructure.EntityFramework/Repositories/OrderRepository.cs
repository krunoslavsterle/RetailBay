using Microsoft.Extensions.Logging;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// OrderRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.TenantDB.Order}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IOrderRepository" />
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrderRepository(TenantDBContext context, ILogger<OrderRepository> logger) : base(context, logger)
        {
        }
    }
}
