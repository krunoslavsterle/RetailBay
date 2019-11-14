using Microsoft.EntityFrameworkCore.Design;
using RetailBay.Domain.Entities.SystemDB;

namespace RetailBay.Infrastructure.EntityFramework.Migrations
{
    public class TenantContextFactory : IDesignTimeDbContextFactory<TenantDBContext>
    {
        public TenantDBContext CreateDbContext(string[] args)
        {
            var tenant = new Tenant
            {
                ConnectionString = "host=localhost;port=5432;database=tenant_first;username=asp_test;password=1234."
            };

            return new TenantDBContext(tenant);
        }
    }
}
