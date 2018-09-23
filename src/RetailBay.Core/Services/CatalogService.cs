using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Core.SharedKernel.Collections;
using RetailBay.Core.SharedKernel.QueryParameters;

namespace RetailBay.Core.Services
{
    public class CatalogService : ICatalogService
    {
        #region Fields

        private readonly IProductRepository _productRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogService"/> class.
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
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public Task<IPagedCollection<Product>> GetProductsPagedAsync(int pageNumber, int pageSize)
        {
            var pagingParameters = new PagingParameters(pageNumber, pageSize);
            return _productRepository.GetPagedAsync(null, null, pagingParameters);
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

        #endregion Methods
    }
}
