using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// UserService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        #region Fields

        private readonly IAddressRepository _addressRepository;
        private readonly IUserAddressRepository _userAddressRepository;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Properties



        #endregion Properties

        #region Methods
        
        /// <summary>
        /// Gets the user addresses by <see cref="AddressType"/>.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        public Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId, AddressType? addressType)
        {
            if (addressType.HasValue)
                return _addressRepository.GetAsync(p => p.UserAddresses.Any(s => s.UserId == userId && s.AddressType == addressType.Value));

            return _addressRepository.GetAsync(p => p.UserAddresses.Any(s => s.UserId == userId));
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

        #endregion Methods
    }
}
