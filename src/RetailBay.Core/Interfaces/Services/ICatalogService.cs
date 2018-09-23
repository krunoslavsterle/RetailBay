using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.SharedKernel.Collections;

namespace RetailBay.Core.Interfaces
{
    public interface ICatalogService
    {
        /// <summary>
        /// Gets the products paged asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<IPagedCollection<Product>> GetProductsPagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Creates the product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task CreateProductAsync(Product product);
    }
}
