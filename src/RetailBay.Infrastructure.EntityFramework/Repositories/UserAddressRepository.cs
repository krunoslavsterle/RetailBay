using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// UserAddressRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.Identity.UserAddress}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IUserAddressRepository" />
    public class UserAddressRepository : Repository<UserAddress>, IUserAddressRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAddressRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserAddressRepository(TenantDBContext context, ILogger<UserAddressRepository> logger) : base(context, logger)
        {
        }

        #endregion Constructors
    }
}
