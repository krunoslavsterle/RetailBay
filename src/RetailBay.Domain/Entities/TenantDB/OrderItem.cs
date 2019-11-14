using System;

namespace RetailBay.Domain.Entities.TenantDB
{
    public class OrderItem : EntityBase
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal ProductPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
