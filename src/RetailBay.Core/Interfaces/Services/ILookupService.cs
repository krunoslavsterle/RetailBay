using System;
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
        /// Gets the one <see cref="TEntity"/> by slug.
        /// </summary>
        /// <param name="abrv">The slug.</param>
        /// <returns></returns>
        Task<TEntity> GetOneBySlugAsync(string slug);

        /// <summary>
        /// Gets the one <see cref="TEntity"/> by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntity> GetOneById(Guid id);

        /// <summary>
        /// Inserts the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Updates the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
