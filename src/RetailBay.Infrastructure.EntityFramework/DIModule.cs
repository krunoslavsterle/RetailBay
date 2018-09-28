using Microsoft.Extensions.DependencyInjection;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Infrastructure.EntityFramework.Repositories;

namespace RetailBay.Infrastructure.EntityFramework
{
    public static class DIModule
    {
        public static IServiceCollection AddInfrastructureEFDependencies(this IServiceCollection services)
        {
            services.AddDbContext<TenantDBContext>(ServiceLifetime.Scoped);
            services.AddDbContext<SystemDBContext>(ServiceLifetime.Scoped);
            services.AddDbContext<IdentityDBContext>(ServiceLifetime.Scoped);

            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
