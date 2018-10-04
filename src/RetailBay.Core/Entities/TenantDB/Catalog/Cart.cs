using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RetailBay.Core.Entities.Identity;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("cart")]
    public class Cart : EntityBase
    {
        public Guid? UserId { get; set; }

        public ApplicationUser User { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
