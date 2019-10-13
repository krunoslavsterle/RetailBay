using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Interfaces.Repositories;
using SaasKit.Multitenancy;

namespace RetailBay.Infrastructure.Multitenancy
{
    /// <summary>
    /// TenantResolver implementation.
    /// </summary>
    public class TenantResolver : MemoryCacheTenantResolver<Tenant>
    {
        private readonly ISystemRepository _systemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantResolver"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="systemRepository">The system repository.</param>
        public TenantResolver(IMemoryCache cache, ILoggerFactory loggerFactory, ISystemRepository systemRepository)
            : base(cache, loggerFactory)
        {
            _systemRepository = systemRepository;
        }

        /// <summary>
        /// Gets the context identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected override string GetContextIdentifier(HttpContext context)
        {
            return context.Request.Host.Value.ToLower();
        }

        /// <summary>
        /// Gets the tenant identifiers.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<Tenant> context)
        {
            return context.Tenant.HostName.Split(';');
        }

        /// <summary>
        /// Resolves the tenant asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected override async Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<Tenant> tenantContext = null;
            var hostName = GetContextIdentifier(context);

            var tenants = await _systemRepository.GetAllTenantsAsync();
            foreach(var ten in tenants)
            {
                var hosts = ten.HostName.Split(';');
                if (hosts.Contains(hostName))
                {
                    tenantContext = new TenantContext<Tenant>(ten);
                    break;
                }
            }
            
            // TODO: Check how to handle case if no tenant was found.
            return tenantContext;
        }
    }
}
