using System;

namespace RetailBay.Domain.Entities.TenantDB
{
    public class ProductPrice : EntityBase
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }
    }
}
