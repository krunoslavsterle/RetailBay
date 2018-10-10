using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// OrderService contract.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Gets list of all <see cref="Order"/> asynchronous.
        /// </summary>
        /// <returns>List of all <see cref="Order"/> asynchronous.</returns>
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        /// <summary>
        /// Creates the order for user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="shippingAddressId">The shipping address identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No cart items found</exception>
        Task CreateOrderForUserAsync(Guid userId, Guid cartId, Guid shippingAddressId);
    }
}
