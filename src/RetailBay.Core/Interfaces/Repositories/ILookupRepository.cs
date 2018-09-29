using RetailBay.Core.Entities;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// LookupRepository contract.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{TEntity}" />
    public interface ILookupRepository<TEntity> : IRepository<TEntity> 
        where TEntity: LookupEntityBase
    {
    }
}
