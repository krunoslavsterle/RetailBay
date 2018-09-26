using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.SharedKernel.Extensions;

namespace RetailBay.Infrastructure.EntityFramework
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly string _connectionString;
        public IdentityDBContext(Tenant tenant) : base(new DbContextOptionsBuilder().UseNpgsql(tenant.ConnectionString).Options)
        {
            _connectionString = tenant.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseSnakeCaseNamingConvention();
        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var defaultUser = new ApplicationUser { UserName = "ksterle", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, "Password1.");

            var adminRole = new ApplicationRole { Name = "Administrator" };
            await roleManager.CreateAsync(adminRole);

            await userManager.AddToRoleAsync(defaultUser, adminRole.Name);
        }
    }
}
