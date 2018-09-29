using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// ProductCategoryRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.TenantDB.ProductCategory}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IProductCategoryRepository" />
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductCategoryRepository(TenantDBContext context) : base(context)
        {
        }

        #endregion Constructors
    }
}
