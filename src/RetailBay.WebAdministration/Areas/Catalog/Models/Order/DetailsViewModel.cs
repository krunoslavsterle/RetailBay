using System;
using System.Collections.Generic;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.TenantDB;

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
