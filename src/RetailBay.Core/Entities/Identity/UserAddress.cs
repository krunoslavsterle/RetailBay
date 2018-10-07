using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.Identity
{
    [Table("user_address")]
    public class UserAddress : EntityBase
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        [Required]
        public AddressType AddressType { get; set; }

        public ApplicationUser User { get; set; }
        public Address Address { get; set; }
    }
}
