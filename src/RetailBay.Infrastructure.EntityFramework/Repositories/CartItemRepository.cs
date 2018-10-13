using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// CartItemRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.TenantDB.CartItem}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.ICartItemRepository" />
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CartItemRepository(TenantDBContext context, ILogger<CartItemRepository> logger) : base(context, logger)
        {
        }

        #endregion Constructors
    }
}
