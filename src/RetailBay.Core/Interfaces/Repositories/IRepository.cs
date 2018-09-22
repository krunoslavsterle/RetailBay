using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RetailBay.Core.SharedKernel.QueryParameters;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// Repository contract.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets all <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>All <see cref="TEntity"/> asynchronous.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(ISortingParameters orderBy = null, params string[] includeProperties);

        /// <summary>
        /// Gets one <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>One <see cref="TEntity"/> asynchronous.</returns>
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includeProperties);

        /// <summary>
        /// Gets the list of <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The list of <see cref="TEntity"/> asynchronous.</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, ISortingParameters orderBy = null, params string[] includeProperties);

        /// <summary>
        /// Gets the list of <see cref="TEntity"/> paged asynchronous.
        /// </summary>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The list of <see cref="TEntity"/> paged asynchronous.</returns>
        Task<IEnumerable<TEntity>> GetPagedAsync(IPagingParameters pagingParameters, Expression<Func<TEntity, bool>> filter = null, ISortingParameters orderBy = null, params string[] includeProperties);

        /// <summary>
        /// Gets the count of <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The count of <see cref="TEntity"/> asynchronous.</returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Inserts the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="save">if set to <c>true</c> [save].</param>
        void InsertAsync(TEntity entity, bool save = true);

        /// <summary>
        /// Updates the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="save">if set to <c>true</c> [save].</param>
        void UpdateAsync(TEntity entity, bool save = true);
        
        /// <summary>
        /// Deletes the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="save">if set to <c>true</c> [save].</param>
        void DeleteAsync(object id, bool save = true);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
