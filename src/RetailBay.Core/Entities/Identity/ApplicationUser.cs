using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Core.Entities.Identity
{
    [Table("identity_user")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Cart Cart { get; set; }
    }
}
