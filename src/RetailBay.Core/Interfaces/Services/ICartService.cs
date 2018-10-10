using System;
using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// CartService contract.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Gets the cart asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<Cart> GetCartAsync(Guid id, params string[] includeProperties);

        /// <summary>
        /// Gets the number of products in cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        Task<int> GetNumberOfProductsInCartAsync(Guid cartId);

        /// <summary>
        /// Checks the Cart exists.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>True if exists.</returns>
        Task<bool> CheckCartExistsAsync(Guid cartId);

        /// <summary>
        /// Creates the Cart for User.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        Task CreateCartForUserAsync(Guid? userId, Guid cartId);

        /// <summary>
        /// Adds the User to anonymous Cart.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartId or userId</exception>
        /// <exception cref="Exception">Can't find cart. or Cart is belonging to another user</exception>
        Task AddUserToAnonymousCartAsync(Guid userId, Guid cartId);

        /// <summary>
        /// Transfers the anonymous Cart to User.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>Identifier for the user cart.</returns>
        /// <exception cref="ArgumentException">cartId or userId</exception>
        /// <exception cref="Exception">Can't find cart. or Cart is belonging to another user</exception>
        Task<Guid> TransferAnonymousCartToUserAsync(Guid userId, Guid cartId);

        /// <summary>
        /// Removes the Cart Item.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartItemId</exception>
        Task RemoveCartItemAsync(Guid cartItemId);

        /// <summary>
        /// Adds the Product to Cart asynchronous.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartId or productId</exception>
        Task<int> AddProductToCartAsync(Guid cartId, Guid productId);
    }
}
