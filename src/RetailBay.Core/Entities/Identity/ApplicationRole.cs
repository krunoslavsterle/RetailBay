using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RetailBay.Core.Entities.Identity
{
    [Table("identity_role")]
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
