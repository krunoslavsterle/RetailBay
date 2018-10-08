using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Core.SharedKernel.Collections;
using RetailBay.Core.SharedKernel.QueryParameters;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// CatalogService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.ICatalogService" />
    public class CatalogService : ICatalogService
    {
        #region Fields
        
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IOrderRepository _orderRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogService" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="cartRepository">The cart repository.</param>
        /// <param name="cartItemRepository">The cart item repository.</param>
        /// <param name="addressRepository">The address repository.</param>
        /// <param name="userAddressRepository">The user address repository.</param>
        /// <param name="orderRepository">The order repository.</param>
        public CatalogService(
            IProductRepository productRepository, 
            ICartRepository cartRepository, 
            ICartItemRepository cartItemRepository, 
            IAddressRepository addressRepository, 
            IUserAddressRepository userAddressRepository,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _addressRepository = addressRepository;
            _userAddressRepository = userAddressRepository;
            _orderRepository = orderRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the products paged asynchronous.
        /// </summary>
        /// <param name="sortingParameters">The sorting parameters.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public Task<IPagedCollection<Product>> GetProductsPagedAsync(ISortingParameters sortingParameters, int pageNumber, int pageSize)
        {
            var pagingParameters = new PagingParameters(pageNumber, pageSize);
            return _productRepository.GetPagedAsync(null, sortingParameters, pagingParameters, nameof(Product.ProductPrice));
        }

        /// <summary>
        /// Creates the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public Task CreateProductAsync(Product product)
        {
            return _productRepository.InsertAsync(product);
        }

        /// <summary>
        /// Edits the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public Task EditProductAsync(Product product)
        {
            return _productRepository.UpdateAsync(product);
        }

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<Product> GetProductAsync(Guid id)
        {
            return _productRepository.GetOneAsync(p => p.Id == id);
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        public Task DeleteProductAsync(Guid productId)
        {
            return _productRepository.DeleteAsync(productId);
        }

        /// <summary>
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
        public async Task<int> AddProductToCartAsync(Guid cartId, Guid productId)
        {
            if (cartId == Guid.Empty) throw new ArgumentException(nameof(cartId));
            if (productId == Guid.Empty) throw new ArgumentException(nameof(productId));

            var cartsCount = await _cartRepository.GetCountAsync(p => p.Id == cartId);
            if (cartsCount == 0)
            {
                
            }

            var cart = await _cartRepository.GetOneAsync(p => p.Id == cartId);

            var entity = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cartId,
                ProductId = productId
            };

            await _cartItemRepository.InsertAsync(entity);
            return await _cartItemRepository.GetCountAsync(p => p.CartId == cartId);
        }

        /// <summary>
        /// Removes the cart item.
        /// </summary>
        /// <param name="cartItemId">The cart item identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">cartItemId</exception>
        public Task RemoveCartItem(Guid cartItemId)
        {
            if (cartItemId == Guid.Empty) throw new ArgumentException(nameof(cartItemId));

            return _cartItemRepository.DeleteAsync(cartItemId);
        }

        /// <summary>
        /// Checks the cart exists.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        public async Task<bool> CheckCartExists(Guid cartId)
        {
            var cartsCount = await _cartRepository.GetCountAsync(p => p.Id == cartId);
            return cartsCount > 0;
        }

        /// <summary>
        /// Creates the cart for user.
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
        /// Gets the number of products in cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        public Task<int> GetNumberOfProductsInCartAsync(Guid cartId)
        {
            return _cartItemRepository.GetCountAsync(p => p.CartId == cartId);
        }

        /// <summary>
        /// Adds the user to anonymous cart.
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
        /// Transfers the anonymous cart to user.
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
        /// Gets the cart asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public Task<Cart> GetCartAsync(Guid id, params string[] includeProperties)
        {
            return _cartRepository.GetOneAsync(p => p.Id == id, includeProperties);
        }

        /// <summary>
        /// Gets the user addresses by <see cref="AddressType"/>.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressType">Type of the address.</param>
        /// <returns></returns>
        public Task<IEnumerable<Address>> GetUserAddresses(Guid userId, AddressType? addressType)
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
        public async Task InsertUserAddress(Address address, Guid userId, AddressType addressType)
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

        /// <summary>
        /// Creates the order for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="shippingAddressId">The shipping address identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No cart items found</exception>
        public async Task CreateOrderForUser(Guid userId, Guid cartId, Guid shippingAddressId)
        {
            var cartItems = await _cartItemRepository.GetAsync(p => p.CartId == cartId, null, $"{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            if (cartItems == null || cartItems.Count() == 0)
                throw new Exception("No cart items found");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ShippingAddressId = shippingAddressId,
                OrderStatus = OrderStatus.New
            };

            var orderItems = new List<OrderItem>();
            var products = cartItems.Select(p => p.Product);
            foreach(var product in products)
            {
                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductPrice = product.ProductPrice.Price,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                });
            }

            order.OrderItems = orderItems;
            order.OrderTotal = orderItems.Sum(p => p.ProductPrice);

            await _orderRepository.InsertAsync(order, false);
            await _cartRepository.DeleteAsync(cartId, false);

            await _cartRepository.SaveAsync();
        }

        #endregion Methods
    }
}
