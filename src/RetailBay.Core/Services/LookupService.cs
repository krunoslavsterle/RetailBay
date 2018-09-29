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
        /// Gets the one <see cref="TEntity"/> by abbreviation.
        /// </summary>
        /// <param name="abrv">The abbreviation.</param>
        /// <returns></returns>
        public Task<TEntity> GetOneByAbrv(string abrv)
        {
            return _lookupRepository.GetOneAsync(p => p.IsDeleted == false && p.Abrv == abrv);
        }

        #endregion Methods
    }
}
