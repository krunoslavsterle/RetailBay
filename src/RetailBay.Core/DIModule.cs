using Microsoft.Extensions.DependencyInjection;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Services;

namespace RetailBay.Core
{
    public static class DIModdule
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));

            services.AddScoped<ILookupServiceFactory, LookupServiceFactory>();

            return services;
        }
    }
}
    