using RetailBay.Core.Entities;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// LookupServiceFactory contract.
    /// </summary>
    public interface ILookupServiceFactory
    {
        /// <summary>
        /// Creates the instance of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The instance of <see cref="T"/>.</returns>
        ILookupService<T> Create<T>() where T : LookupEntityBase;
    }
}
