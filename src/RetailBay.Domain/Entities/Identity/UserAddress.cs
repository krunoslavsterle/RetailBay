using RetailBay.Domain.Enums;
using System;

namespace RetailBay.Domain.Entities.Identity
{
    public class UserAddress : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public AddressType AddressType { get; set; }

        public ApplicationUser User { get; set; }
        public Address Address { get; set; }
    }
}
