using System;
using Microsoft.AspNetCore.Identity;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //public Cart Cart { get; set; }
    }
}
