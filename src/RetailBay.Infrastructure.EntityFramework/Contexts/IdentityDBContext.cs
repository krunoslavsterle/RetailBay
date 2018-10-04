using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly string _connectionString;
        public IdentityDBContext(Tenant tenant) : 
            base(new DbContextOptionsBuilder().UseNpgsql(tenant.ConnectionString).Options)
        {
            _connectionString = tenant.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString, options => options.MigrationsAssembly("RetailBay.Infrastructure.EntityFramework.Migrations"));
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("identity_user");

                //b.HasOne(u => u.Cart)
                //.WithOne(c => c.User)
                //.HasForeignKey<Cart>(c => c.UserId)
                //.IsRequired(false)
                //.OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("identity_role");
            });

            builder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("identity_user_role");
            });
            
            builder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("identity_role_claim");
            });

            builder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("identity_user_claim");
            });

            builder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("identity_user_login");
            });
            
            builder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("identity_user_token");
            });

            builder.UseSnakeCaseNamingConvention(false);
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
