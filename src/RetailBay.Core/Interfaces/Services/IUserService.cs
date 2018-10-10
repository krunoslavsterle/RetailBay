using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailBay.Core.Entities.Identity;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// UserService contract.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the user addresses by <see cref="AddressType"/>.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId, AddressType? addressType);

        /// <summary>
        /// Inserts the user address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        Task InsertUserAddressAsync(Address address, Guid userId, AddressType addressType);
    }
}
