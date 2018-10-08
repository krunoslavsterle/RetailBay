using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Entities.Identity
{
    [Table("identity_user")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Cart Cart { get; set; }
        public IEnumerable<UserAddress> UserAddresses { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
