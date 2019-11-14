using RetailBay.Domain.Entities.Identity;

namespace RetailBay.Core.Interfaces.Repositories
{
    /// <summary>
    /// AddressRepository contract.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IRepository{RetailBay.Core.Entities.Identity.Address}" />
    public interface IAddressRepository : IRepository<Address>
    {
    }
}
