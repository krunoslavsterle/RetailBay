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
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILookupServiceFactory, LookupServiceFactory>();

            services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));

            return services;
        }
    }
}
    