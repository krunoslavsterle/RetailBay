using RetailBay.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RetailBay.Application.Orders.Queries
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderItems = new List<OrderItemDTO>();
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime DateCreated { get; set; }

        public ShippingDTO ShippingAddress { get; set; }
        public IList<OrderItemDTO> OrderItems { get; set; }
    }

    public class ShippingDTO
    {
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public string Product { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
