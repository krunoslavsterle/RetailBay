using Microsoft.Extensions.Logging;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Domain.Entities.Identity;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// UserAddressRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.Identity.UserAddress}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IUserAddressRepository" />
    public class UserAddressRepository : Repository<UserAddress>, IUserAddressRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAddressRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserAddressRepository(TenantDBContext context, ILogger<UserAddressRepository> logger) : base(context, logger)
        {
        }
    }
}
