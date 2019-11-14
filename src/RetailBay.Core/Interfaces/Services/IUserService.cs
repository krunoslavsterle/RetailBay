using RetailBay.Domain.Entities.Identity;
using RetailBay.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// UserService contract.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the addresses for <see cref="ApplicationUser"/> asynchronously. 
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns>List of <see cref="Address"/> for specified <see cref="ApplicationUser"/>.</returns>
        Task<IEnumerable<Address>> GetAddressesForUserAsync(Guid userId, AddressType? addressType);

        /// <summary>
        /// Gets the list of <see cref="UserAddress"/> asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of <see cref="UserAddress"/> asynchronous.</returns>
        Task<IEnumerable<UserAddress>> GetUserAddressesAsync(Guid userId);

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
