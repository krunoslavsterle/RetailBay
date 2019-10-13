using System;
using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// CartService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.ICartService" />
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService" /> class.
        /// </summary>
        /// <param name="cartRepository">The Cart repository.</param>
        /// <param name="cartItemRepository">The CartItem repository.</param>
        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }
        
        /// <summary>
        /// Gets the Cart asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public Task<Cart> GetCartAsync(Guid id, params string[] includeProperties)
        {
            return _cartRepository.GetOneAsync(p => p.Id == id, includeProperties);
        }

        /// <summary>
        /// Gets the user cart asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<Cart> GetUserCartAsync(Guid userId)
        {
            return _cartRepository.GetOneAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Gets the number of products in Cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>Number of products in Cart.</returns>
        public Task<int> GetNumberOfProductsInCartAsync(Guid cartId)
        {
            return _cartItemRepository.GetCountAsync(p => p.CartId == cartId);
        }

        /// <summary>
        /// Checks the Cart exists asynchronous.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> CheckCartExistsAsync(Guid cartId)
        {
            var cartsCount = await _cartRepository.GetCountAsync(p => p.Id == cartId);
            return cartsCount > 0;
        }

        /// <summary>
        /// Creates the Cart for User asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        public Task CreateCartForUserAsync(Guid? userId, Guid cartId)
        {
            var cart = new Cart
            {
                Id = cartId,
                UserId = userId,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            return _cartRepository.InsertAsync(cart);
        }

        /// <summary>
        /// Adds the User to anonymous Cart asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartId or userId</exception>
        /// <exception cref="Exception">Can't find cart. or Cart is belonging to another user</exception>
        public async Task AddUserToAnonymousCartAsync(Guid userId, Guid cartId)
        {
            if (cartId == Guid.Empty) throw new ArgumentException(nameof(cartId));
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository.GetOneAsync(p => p.Id == cartId);
            if (cart == null)
                throw new Exception("Can't find cart.");

            if (cart.UserId.HasValue && cart.UserId.Value != userId)
                throw new Exception("Cart is belonging to another user");

            cart.UserId = userId;
            await _cartRepository.UpdateAsync(cart);
        }

        /// <summary>
        /// Transfers the anonymous Cart to User asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>Identifier for the user cart.</returns>
        /// <exception cref="ArgumentException">cartId or userId</exception>
        /// <exception cref="Exception">Can't find cart. or Cart is belonging to another user</exception>
        public async Task<Guid> TransferAnonymousCartToUserAsync(Guid userId, Guid cartId)
        {
            if (cartId == Guid.Empty) throw new ArgumentException(nameof(cartId));
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            var anonymousCart = await _cartRepository.GetOneAsync(p => p.Id == cartId);
            if (anonymousCart == null)
                throw new Exception("Can't find cart.");

            if (anonymousCart.UserId.HasValue)
            {
                // If we found that the anonymousCart has a user and it is not the current user throw an exception, if it is this user just return it's id, he will use that one.
                if (anonymousCart.UserId.Value != userId)
                    throw new Exception("Cart is belonging to another user");
                else
                    return anonymousCart.Id;
            }

            // Current cart has priority over the old one, so if the user already has a cart we will delete that one.
            var userCart = await _cartRepository.GetOneAsync(p => p.UserId == userId);
            if (userCart != null)
                await _cartRepository.DeleteAsync(userCart.Id);

            anonymousCart.UserId = userId;
            await _cartRepository.UpdateAsync(anonymousCart);
            return anonymousCart.Id;
        }

        /// <summary>
        /// Removes the Cart Item asynchronous.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartItemId</exception>
        public Task RemoveCartItemAsync(Guid cartItemId)
        {
            if (cartItemId == Guid.Empty) throw new ArgumentException(nameof(cartItemId));

            return _cartItemRepository.DeleteAsync(cartItemId);
        }

        /// <summary>
        /// Adds the Product to Cart asynchronous.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartId or productId</exception>
        public async Task<int> AddProductToCartAsync(Guid cartId, Guid productId)
        {
            if (cartId == Guid.Empty) throw new ArgumentException(nameof(cartId));
            if (productId == Guid.Empty) throw new ArgumentException(nameof(productId));

            var entity = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cartId,
                ProductId = productId
            };

            await _cartItemRepository.InsertAsync(entity);
            return await _cartItemRepository.GetCountAsync(p => p.CartId == cartId);
        }
    }
}
