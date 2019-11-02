using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// ShippingAddressService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.IShippingAddressService" />
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICartRepository _cartRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingAddressService" /> class.
        /// </summary>
        /// <param name="addressRepository">The address repository.</param>
        /// <param name="cartRepository">The cart repository.</param>
        public ShippingAddressService(IAddressRepository addressRepository, ICartRepository cartRepository)
        {
            _addressRepository = addressRepository;
            _cartRepository = cartRepository;
        }
        
        /// <summary>
        /// Inserts the shipping address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Task InsertShippingAddressAsync(Address address)
        {
            return _addressRepository.InsertAsync(address);
        }

        /// <summary>
        /// Inserts the shipping address for cart asynchronous.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <exception cref="Exception">Cart not found!</exception>
        public async Task InsertShippingAddressForCartAsync(Address address, Guid cartId)
        {
            var cart = await _cartRepository.GetOneAsync(p => p.Id == cartId);
            if (cart == null)
                throw new Exception("Cart not found!");

            address.Id = Guid.NewGuid();
            await _addressRepository.InsertAsync(address);
            
            cart.ShippingAddressId = address.Id;
            await _cartRepository.UpdateAsync(cart);
        }

        /// <summary>
        /// Gets the shipping address for cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        public Task<Address> GetShippingAddressForCartAsync(Guid cartId)
        {
            return _addressRepository.GetOneAsync(p => p.Carts.Any(c => c.Id == cartId));
        }
    }
}
