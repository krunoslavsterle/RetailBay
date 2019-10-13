using Microsoft.EntityFrameworkCore;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Infrastructure.EntityFramework.Configurations.SystemDB;

namespace RetailBay.Infrastructure.EntityFramework
{
    /// <summary>
    /// SystemDBContext implementation.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class SystemDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemDBContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TenantConfiguration());
            builder.UseSnakeCaseNamingConvention(true);
        }
    }
}
