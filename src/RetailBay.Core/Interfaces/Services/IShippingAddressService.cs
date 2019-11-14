using RetailBay.Domain.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// ShippingAddressService contract.
    /// </summary>
    public interface IShippingAddressService
    {
        /// <summary>
        /// Inserts the shipping address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Task InsertShippingAddressAsync(Address address);

        /// <summary>
        /// Gets the shipping address for cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        Task<Address> GetShippingAddressForCartAsync(Guid cartId);

        /// <summary>
        /// Inserts the shipping address for cart asynchronous.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <exception cref="Exception">Cart not found!</exception>
        Task InsertShippingAddressForCartAsync(Address address, Guid cartId);

        /// <summary>
        /// Updates the shipping address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Task UpdateShippingAddress(Address address);
    }
}
