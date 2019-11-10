using Microsoft.EntityFrameworkCore;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.SystemDb;
using System;

namespace RetailBay.Infrastructure.EntityFramework.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitializeDevEnvironment();
            Console.Read();
        }

        private static void InitializeDevEnvironment()
        {
            var systemDBContextFactory = new SystemDBContextFactory();
            var tenantDBContextFactory = new TenantContextFactory();

            Console.WriteLine("Creating SystemDB context...");
            var systemDBContext = systemDBContextFactory.CreateDbContext(null);
            Console.WriteLine("SystemDB context created!\n");

            Console.WriteLine("Creating TenantDB context...");
            var tenantDBContext = tenantDBContextFactory.CreateDbContext(null);
            Console.WriteLine("TenantDB context created!\n");

            var tenantConnectionString = tenantDBContext.Database.GetDbConnection().ConnectionString;
            tenantConnectionString = tenantConnectionString.Replace("localhost", "retailbay_postgres_server");

            Console.WriteLine("Applying migrations to SystemDB...");
            systemDBContext.Database.Migrate();
            Console.WriteLine("SystemDB migrations applied!\n");

            Console.WriteLine("Applying migrations to TenantDB...");
            tenantDBContext.Database.Migrate();
            Console.WriteLine("TenantDB migrations applied!\n");

            Console.WriteLine("Seeding SystemDB database...");
            Console.WriteLine("Adding Tenant...");
            AddTenantToSystemDB(systemDBContext, tenantConnectionString);
            Console.WriteLine("Tenant added!");
            Console.WriteLine("SystemDB seed completed!\n");

            Console.WriteLine("Seeding TenantDB database...");
            Console.WriteLine("Adding Identity Roles to TenantDB database...");
            AddIdentityRolesToTenantDB(tenantDBContext);
            Console.WriteLine("Identity Roles added!\n");
            Console.WriteLine("TenantDB seed completed!\n");
        }

        private static void AddTenantToSystemDB(SystemDBContext systemDBContext, string tenantConnectionString)
        {
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Tenant_one",
                ConnectionString = tenantConnectionString,
                HostName = "tenant1.localhost:44371;tenant1.localhost:51468;tenant1.localhost:44372",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            systemDBContext.Tenants.Add(tenant);
            systemDBContext.SaveChanges();
        }

        private static void AddIdentityRolesToTenantDB(TenantDBContext tenantDBContext)
        {
            tenantDBContext.Roles.Add(new ApplicationRole
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

            tenantDBContext.Roles.Add(new ApplicationRole
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            });

            tenantDBContext.SaveChanges();
        }
    }
}
