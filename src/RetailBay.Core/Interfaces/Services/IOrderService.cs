using RetailBay.Domain.Entities.TenantDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Gets the <see cref="Order" /> by id asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The <see cref="Order" /></returns>
        Task<Order> GetOrderAsync(Guid id, params string[] includeProperties);

        /// <summary>
        /// Gets the orders for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersForUserAsync(Guid userId);

        /// <summary>
        /// Creates the order for user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No cart items found</exception>
        Task CreateOrderForUserAsync(Guid? userId, Guid cartId);
    }
}
