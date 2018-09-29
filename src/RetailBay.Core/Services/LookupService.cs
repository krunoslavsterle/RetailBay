using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// LookupService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.ILookupService" />
    public class LookupService : ILookupService
    {
        #region Fields

        private readonly IProductCategoryRepository _productCategoryRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupService"/> class.
        /// </summary>
        /// <param name="productCategoryRepository">The product category repository.</param>
        public LookupService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the product categories.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            return _productCategoryRepository.GetAllAsync();
        }

        #endregion Methods
    }
}
