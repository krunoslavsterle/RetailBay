using System.Collections.Generic;
using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// LookupService contract.
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// Gets the product categories.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductCategory>> GetProductCategories();
    }
}
