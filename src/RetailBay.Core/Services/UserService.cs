using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Domain.Entities.Identity;
using RetailBay.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// UserService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserAddressRepository _userAddressRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userAddressRepository">The user address repository.</param>
        /// <param name="addressRepository">The address repository.</param>
        public UserService(IUserAddressRepository userAddressRepository, IAddressRepository addressRepository)
        {
            _userAddressRepository = userAddressRepository;
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// Gets the addresses for <see cref="ApplicationUser"/> asynchronously. 
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns>List of <see cref="Address"/> for specified <see cref="ApplicationUser"/>.</returns>
        public Task<IEnumerable<Address>> GetAddressesForUserAsync(Guid userId, AddressType? addressType)
        {
            if (addressType.HasValue)
                return _addressRepository.GetAsync(p => p.UserAddresses.Any(s => s.UserId == userId && s.AddressType == addressType.Value));

            return _addressRepository.GetAsync(p => p.UserAddresses.Any(s => s.UserId == userId));
        }

        /// <summary>
        /// Gets the list of <see cref="UserAddress"/> asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of <see cref="UserAddress"/> asynchronous.</returns>
        public Task<IEnumerable<UserAddress>> GetUserAddressesAsync(Guid userId)
        {
            return _userAddressRepository.GetAsync(p => p.UserId == userId, null, nameof(UserAddress.Address));
        }

        /// <summary>
        /// Inserts the user address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        public async Task InsertUserAddressAsync(Address address, Guid userId, AddressType addressType)
        {
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            address.Id = Guid.NewGuid();
            var userAddress = new UserAddress
            {
                Id = Guid.NewGuid(),
                AddressType = addressType,
                UserId = userId,
                AddressId = address.Id,
                Address = address
            };

            await _userAddressRepository.InsertAsync(userAddress);
        }
    }
}
