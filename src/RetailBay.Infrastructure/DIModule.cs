using Microsoft.Extensions.DependencyInjection;
using RetailBay.Core.Interfaces;
using RetailBay.Infrastructure.Logging;

namespace RetailBay.Infrastructure
{
    public static class DIModdule
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
