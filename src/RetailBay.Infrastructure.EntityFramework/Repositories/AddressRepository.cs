using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// AddressRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.Identity.Address}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IAddressRepository" />
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AddressRepository(TenantDBContext context) : base(context)
        {
        }

        #endregion Constructors
    }
}
