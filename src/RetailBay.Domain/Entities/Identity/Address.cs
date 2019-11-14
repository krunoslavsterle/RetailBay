using RetailBay.Domain.Entities.TenantDB;
using System.Collections.Generic;

namespace RetailBay.Domain.Entities.Identity
{
    public class Address : EntityBase
    {
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public IEnumerable<UserAddress> UserAddresses { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    }
}
