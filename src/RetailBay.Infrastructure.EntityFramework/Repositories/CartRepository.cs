using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// CartRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.TenantDB.Cart}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.ICartRepository" />
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CartRepository(TenantDBContext context, ILogger<CartRepository> logger) : base(context, logger)
        {
        }
    }
}
