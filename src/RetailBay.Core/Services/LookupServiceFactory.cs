using RetailBay.Core.Interfaces;
using RetailBay.Domain.Entities;
using System;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// LookupServiceFactory implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.ILookupServiceFactory" />
    public class LookupServiceFactory : ILookupServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupServiceFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public LookupServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates the instance of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The instance of <see cref="T"/>.</returns>
        public ILookupService<T> Create<T>() where T : LookupEntityBase
        {
            return _serviceProvider.GetService(typeof(ILookupService<T>)) as ILookupService<T>;
        }
    }
}
