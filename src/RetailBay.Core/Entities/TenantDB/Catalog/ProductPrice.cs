using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("product_price")]
    public class ProductPrice : EntityBase
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public Product Product { get; set; }
    }
}
