using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RetailBay.Core.Entities.Identity;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("order")]
    public class Order : EntityBase
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ShippingAddressId { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        public ApplicationUser User { get; set; }
        public Address ShippingAddress { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
