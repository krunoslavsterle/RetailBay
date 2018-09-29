using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// ProductCategoryRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.TenantDB.ProductCategory}" />
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
    }
}
