using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("order_item")]
    public class OrderItem : EntityBase
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
