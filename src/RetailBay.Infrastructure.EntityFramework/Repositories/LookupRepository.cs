using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// LookupRepository implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{TEntity}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.ILookupRepository{TEntity}" />
    public class LookupRepository<TEntity> : Repository<TEntity>, ILookupRepository<TEntity>
        where TEntity: LookupEntityBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LookupRepository(TenantDBContext context, ILogger<LookupRepository<TEntity>> logger) : base(context, logger)
        {
        }

        #endregion Constructors
    }
}
