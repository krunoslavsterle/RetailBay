using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Core.SharedKernel.Collections;
using RetailBay.Core.SharedKernel.QueryParameters;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// CatalogService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.ICatalogService" />
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogService" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="logger">The logger.</param>
        public CatalogService(IProductRepository productRepository, ILogger<CatalogService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets the products paged asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortingParameters">The sorting parameters.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public Task<IPagedCollection<Product>> GetProductsPagedAsync(Expression<Func<Product, bool>> filter, ISortingParameters sortingParameters, int pageNumber, int pageSize)
        {
            _logger.LogDebug("{Method} - {PageNumber}, {PageSize}", nameof(CatalogService.GetProductsPagedAsync), pageNumber, pageSize);

            var pagingParameters = new PagingParameters(pageNumber, pageSize);
            return _productRepository.GetPagedAsync(filter, sortingParameters, pagingParameters, nameof(Product.ProductPrice));
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
            return _productRepository.GetOneAsync(p => p.Id == id, nameof(Product.ProductPrice));
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
    }
}
