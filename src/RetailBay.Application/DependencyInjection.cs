using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RetailBay.Application.Common.Behaviours;
using System.Reflection;

namespace RetailBay.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
