using System.Collections.Generic;
using System.Threading.Tasks;
using RetailBay.Core.Entities;

namespace RetailBay.Core.Interfaces
{
    /// <summary>
    /// LookupService contract.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface ILookupService<TEntity> where TEntity: LookupEntityBase
    {
        /// <summary>
        /// Gets all <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets the one <see cref="TEntity"/> by abbreviation.
        /// </summary>
        /// <param name="abrv">The abbreviation.</param>
        /// <returns></returns>
        Task<TEntity> GetOneByAbrv(string abrv);
    }
}
