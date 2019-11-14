using Microsoft.AspNetCore.Identity;
using RetailBay.Domain.Entities.TenantDB;
using System;
using System.Collections.Generic;

namespace RetailBay.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Cart Cart { get; set; }
        public IEnumerable<UserAddress> UserAddresses { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
