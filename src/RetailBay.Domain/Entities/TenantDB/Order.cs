using RetailBay.Domain.Entities.Identity;
using RetailBay.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RetailBay.Domain.Entities.TenantDB
{
    public class Order : EntityBase
    {
        public Guid? UserId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal OrderTotal { get; set; }

        public ApplicationUser User { get; set; }
        public Address ShippingAddress { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
