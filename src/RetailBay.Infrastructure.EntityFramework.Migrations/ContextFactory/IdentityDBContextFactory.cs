using Microsoft.EntityFrameworkCore.Design;
using RetailBay.Core.Entities.SystemDb;

namespace RetailBay.Infrastructure.EntityFramework.Migrations
{
    public class IdentityDBContextFactory : IDesignTimeDbContextFactory<IdentityDBContext>
    {
        public IdentityDBContext CreateDbContext(string[] args)
        {
            var tenant = new Tenant
            {
                ConnectionString = "host=localhost;port=5432;database=tenant_first;username=asp_test;password=1234."
            };

            return new IdentityDBContext(tenant);
        }
    }
}
