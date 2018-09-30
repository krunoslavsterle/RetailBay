using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailBay.Core.Entities;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// LookupService implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="RetailBay.Core.Interfaces.ILookupService{TEntity}" />
    public class LookupService<TEntity> : ILookupService<TEntity> where TEntity: LookupEntityBase
    {
        #region Fields

        private readonly ILookupRepository<TEntity> _lookupRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupService"/> class.
        /// </summary>
        /// <param name="productCategoryRepository">The product category repository.</param>
        public LookupService(ILookupRepository<TEntity> lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets all <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _lookupRepository.GetAsync(p => p.IsDeleted == false);
        }

        /// <summary>
        /// Gets the one <see cref="TEntity"/> by slug asynchronous.
        /// </summary>
        /// <param name="abrv">The slug.</param>
        /// <returns></returns>
        public Task<TEntity> GetOneBySlugAsync(string slug)
        {
            return _lookupRepository.GetOneAsync(p => p.IsDeleted == false && p.Slug == slug); 
        }

        /// <summary>
        /// Gets the one <see cref="TEntity"/> by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<TEntity> GetOneById(Guid id)
        {
            return _lookupRepository.GetOneAsync(p => p.IsDeleted == false && p.Id == id);
        }

        /// <summary>
        /// Inserts the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task InsertAsync(TEntity entity)
        {
            return _lookupRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Updates the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task UpdateAsync(TEntity entity)
        {
            return _lookupRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes the <see cref="TEntity"/> asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var lkp = await GetOneById(id);
            lkp.IsDeleted = true;
            await _lookupRepository.UpdateAsync(lkp);
        }

        #endregion Methods
    }
}
