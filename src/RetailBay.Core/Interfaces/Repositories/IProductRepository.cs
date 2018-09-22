using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// ProductRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.TenantDB.Product}" />
    public interface IProductRepository : IRepository<Product>
    {
    }
}
