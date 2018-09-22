using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailBay.Core.Entities;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private SystemDBContext _systemDBContext = new SystemDBContext();

        public Task<List<Tenant>> GetAllTenantsAsync()
        {
            return _systemDBContext.Tenants.ToListAsync();
        }
    }
}
