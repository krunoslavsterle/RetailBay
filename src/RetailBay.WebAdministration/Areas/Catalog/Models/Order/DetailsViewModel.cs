using RetailBay.Domain.Entities.Identity;
using RetailBay.Domain.Entities.TenantDB;
using RetailBay.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RetailBay.WebAdministration.Areas.Catalog.Models.Order
{
    public class DetailsViewModel
    {
        public OrderDTO Order { get; set; }

        public class OrderDTO
        {
            public Guid Id { get; set; }
            public Address ShippingAddress { get; set; }
            public decimal OrderTotal { get; set; }
            public OrderStatus OrderStatus { get; set; }
            public IEnumerable<OrderItem> OrderItems { get; set; }
        }
    }
}
