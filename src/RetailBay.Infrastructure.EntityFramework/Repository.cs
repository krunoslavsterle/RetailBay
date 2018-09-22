using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Core.SharedKernel.Collections;
using RetailBay.Core.SharedKernel.QueryParameters;

namespace RetailBay.Infrastructure.EntityFramework
{
    /// <summary>
    /// Entity Framework Generic Repository implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{TEntity}" />
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        #region Fields

        private readonly DbContext _context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets all <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="sortingParameters">The sorting parameters..</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// All <see cref="TEntity" /> asynchronous.
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISortingParameters sortingParameters = null, params string[] includeProperties)
        {
            return await GetQueryable(null, sortingParameters, null, includeProperties).ToListAsync();
        }

        /// <summary>
        /// Gets one <see cref="TEntity" /> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// One <see cref="TEntity" /> asynchronous.
        /// </returns>
        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includeProperties)
        {
            return await GetQueryable(filter, null, null, includeProperties).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets the list of <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortingParameters">The sorting parameters.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The list of <see cref="TEntity"/> asynchronous.</returns>
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            ISortingParameters sortingParameters = null, 
            params string[] includeProperties)
        {
            return await GetQueryable(filter, sortingParameters, null, includeProperties).ToListAsync();
        }

        /// <summary>
        /// Gets the paged list of <see cref="TEntity" /> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortingParameters">The sorting parameters.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// The paged list of <see cref="!:TEntity" /> paged asynchronous.
        /// </returns>
        public async Task<IPagedCollection<TEntity>> GetPagedAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            ISortingParameters sortingParameters = null, 
            IPagingParameters pagingParameters = null, 
            params string[] includeProperties)
        {
            // NOTE: EF Core does not support multiple parallel operations being run on the same context instance. 
            var totalCount = await GetCountAsync();
            var result = await GetQueryable(filter, sortingParameters, pagingParameters, includeProperties).ToListAsync();

            return new PagedCollection<TEntity>(result, totalCount, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        /// <summary>
        /// Gets the count of <see cref="!:TEntity" /> asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The count of <see cref="!:TEntity" /> asynchronous.
        /// </returns>
        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter, null, null).CountAsync();
        }

        /// <summary>
        /// Inserts the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="save">if set to <c>true</c> [save].</param>
        public async Task InsertAsync(TEntity entity, bool save = true)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.AddAsync<TEntity>(entity);
            if (save)
                await SaveAsync();
        }

        /// <summary>
        /// Updates the <see cref="!:TEntity" /> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="save">if set to <c>true</c> [save].</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, bool save = true)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Update<TEntity>(entity);
            if (save)
                await SaveAsync();
        }

        /// <summary>
        /// Deletes the <see cref="!:TEntity" /> asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="save">if set to <c>true</c> [save].</param>
        /// <returns></returns>
        public async Task DeleteAsync(object id, bool save = true)
        {
            var entity = await _context.FindAsync<TEntity>(id);

            if (entity != null)
            {
                _context.Remove(entity);
                if (save)
                    await SaveAsync();
            }
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        private IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter, 
            ISortingParameters sortingParameters, 
            IPagingParameters pagingParameters, 
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // Handle filtering;
            if (filter != null)
                query = query.Where(filter);

            // Handle sorting.
            if (sortingParameters != null && sortingParameters.Sorters?.Count > 0)
            {
                for (int i = 0; i < sortingParameters.Sorters.Count; i++)
                {

                    if (sortingParameters.Sorters[i].IsAscending)
                        query = query.OrderBy(sortingParameters.Sorters[i].OrderBy);
                    else
                        query = query.OrderByDescending(sortingParameters.Sorters[i].OrderBy);
                }
            }

            // Handle paging.
            if (pagingParameters != null)
                query = query.Skip(pagingParameters.Skip).Take(pagingParameters.PageSize);

            // Handle include properties.
            if (includeProperties != null && includeProperties.Length > 0)
            {
                for (int i = 0; i < includeProperties.Length; i++)
                    query = query.Include(includeProperties[i]);
            }

            return query;
        }
        
        #endregion Methods
    }
}
