using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RetailBay.Infrastructure.EntityFramework.Migrations
{
    public class SystemDBContextFactory : IDesignTimeDbContextFactory<SystemDBContext>
    {
        public SystemDBContext CreateDbContext(string[] args)
        {
            string connectionString = "host=localhost;port=5432;database=retail_bay_system;username=asp_test;password=1234.";

            DbContextOptionsBuilder<SystemDBContext> optionsBuilder = new DbContextOptionsBuilder<SystemDBContext>()
                .UseNpgsql(connectionString, p =>
                    p.MigrationsAssembly("RetailBay.Infrastructure.EntityFramework.Migrations"));

            return new SystemDBContext(optionsBuilder.Options);
        }
    }
}
