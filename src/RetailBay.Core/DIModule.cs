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
            services.AddScoped<ILookupService, LookupService>();

            return services;
        }
    }
}
    