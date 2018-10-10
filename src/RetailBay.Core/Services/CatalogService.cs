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

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogService" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public CatalogService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
         
        #endregion Methods
    }
}
