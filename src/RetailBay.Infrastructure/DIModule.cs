using Microsoft.Extensions.DependencyInjection;

namespace RetailBay.Infrastructure
{
    public static class DIModdule
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            return services;
        }
    }
}
