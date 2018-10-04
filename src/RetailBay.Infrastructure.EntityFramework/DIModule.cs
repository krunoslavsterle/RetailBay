using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Infrastructure.EntityFramework.Repositories;

namespace RetailBay.Infrastructure.EntityFramework
{
    public static class DIModule
    {
        public static IServiceCollection AddInfrastructureEFDependencies(this IServiceCollection services)
        {
            services.AddDbContext<SystemDBContext>((serviceProvider, builder) => 
            {
                builder.UseNpgsql(Environment.GetEnvironmentVariable("SYSTEM_DB_CONNECTION_STRING"));
            }, ServiceLifetime.Scoped);

            services.AddDbContext<TenantDBContext>(ServiceLifetime.Scoped);

            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(ILookupRepository<>), typeof(LookupRepository<>));


            return services;
        }
    }
}
