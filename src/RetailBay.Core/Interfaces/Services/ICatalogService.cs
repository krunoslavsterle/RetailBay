using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.SharedKernel.Collections;
using RetailBay.Core.SharedKernel.QueryParameters;

namespace RetailBay.Core.Interfaces
{
    public interface ICatalogService
    {
        /// <summary>
        /// Gets the products paged asynchronous.
        /// </summary>
        /// <param name="sortingParameters">The sorting parameters.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<IPagedCollection<Product>> GetProductsPagedAsync(ISortingParameters sortingParameters, int pageNumber, int pageSize);

        /// <summary>
        /// Creates the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task CreateProductAsync(Product product);

        /// <summary>
        /// Edits the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task EditProductAsync(Product product);

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task DeleteProductAsync(Guid productId);

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Product> GetProductAsync(Guid id);

        // <summary>
        /// Adds the product to cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// cartId
        /// or
        /// productId
        /// </exception>
        Task<int> AddProductToCartAsync(Guid cartId, Guid productId);

        /// <summary>
        /// Removes the cart item.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartItemId</exception>
        Task RemoveCartItem(Guid cartItemId);

        /// <summary>
        /// Checks the cart exists.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        Task<bool> CheckCartExists(Guid cartId);

        /// <summary>
        /// Creates the cart for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        Task CreateCartForUserAsync(Guid? userId, Guid cartId);

        /// <summary>
        /// Gets the number of products in cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        Task<int> GetNumberOfProductsInCartAsync(Guid cartId);

        /// <summary>
        /// Adds the user to anonymous cart.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartId or userId</exception>
        /// <exception cref="Exception">Can't find cart. or Cart is belonging to another user</exception>
        Task AddUserToAnonymousCartAsync(Guid userId, Guid cartId);

        /// <summary>
        /// Transfers the anonymous cart to user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>Identifier for the user cart.</returns>
        /// <exception cref="ArgumentException">cartId or userId</exception>
        /// <exception cref="Exception">Can't find cart. or Cart is belonging to another user</exception>
        Task<Guid> TransferAnonymousCartToUserAsync(Guid userId, Guid cartId);

        /// <summary>
        /// Gets the cart asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<Cart> GetCartAsync(Guid id, params string[] includeProperties);

        /// <summary>
        /// Gets the user addresses by <see cref="AddressType"/>.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        Task<IEnumerable<Address>> GetUserAddresses(Guid userId, AddressType? addressType);

        /// <summary>
        /// Inserts the user address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        Task InsertUserAddress(Address address, Guid userId, AddressType addressType);

        /// <summary>
        /// Creates the order for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="shippingAddressId">The shipping address identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No cart items found</exception>
        Task CreateOrderForUser(Guid userId, Guid cartId, Guid shippingAddressId);
    }
}
