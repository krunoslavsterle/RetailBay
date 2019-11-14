using RetailBay.Domain.Entities.Identity;
using System;
using System.Collections.Generic;

namespace RetailBay.Domain.Entities.TenantDB
{
    public class Cart : EntityBase
    {
        public Guid? UserId { get; set; }
        public Guid? ShippingAddressId { get; set; }

        public ApplicationUser User { get; set; }
        public Address ShippingAddress { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
