using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("cart_item")]
    public class CartItem : EntityBase
    {
        [Required]
        public Guid CartId { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
