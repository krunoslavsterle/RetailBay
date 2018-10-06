using System;
using System.Linq;
using System.Threading.Tasks;
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

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogService" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="cartRepository">The cart repository.</param>
        /// <param name="cartItemRepository">The cart item repository.</param>
        public CatalogService(IProductRepository productRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
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
            return _productRepository.GetPagedAsync(null, sortingParameters, pagingParameters);
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
        public async Task<int> AddProductToCart(Guid cartId, Guid productId)
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
        public Task CreateCartForUser(Guid? userId, Guid cartId)
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
        public Task<int> GetNumberOfProductsInCart(Guid cartId)
        {
            return _cartItemRepository.GetCountAsync(p => p.CartId == cartId);
        }

        #endregion Methods
    }
}
