using RetailBay.Domain.Entities.SystemDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// SystemRepository contract.
    /// </summary>
    public interface ISystemRepository
    {
        /// <summary>
        /// Get all tenants from the DB asynchronous.
        /// </summary>
        /// <returns>
        /// List of <see cref="Tenant" />.
        /// </returns>
        Task<List<Tenant>> GetAllTenantsAsync();
    }
}
