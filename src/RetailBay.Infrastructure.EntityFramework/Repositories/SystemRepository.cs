using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private readonly SystemDBContext _systemDBContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public SystemRepository(SystemDBContext context)
        {
            _systemDBContext = context;
        }

        public Task<List<Tenant>> GetAllTenantsAsync()
        {
            return _systemDBContext.Tenants.ToListAsync();
        }
    }
}
