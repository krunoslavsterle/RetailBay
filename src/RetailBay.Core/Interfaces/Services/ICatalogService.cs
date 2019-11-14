using RetailBay.Common.Collections;
using RetailBay.Common.QueryParameters;
using RetailBay.Domain.Entities.TenantDB;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// CatalogService contract.
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Gets the products paged asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortingParameters">The sorting parameters.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<IPagedCollection<Product>> GetProductsPagedAsync(Expression<Func<Product, bool>> filter, ISortingParameters sortingParameters, int pageNumber, int pageSize);

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

        /// <summary>
        /// Gets the product by slug asynchronous.
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <returns></returns>
        Task<Product> GetProductBySlugAsync(string slug);
    }
}
