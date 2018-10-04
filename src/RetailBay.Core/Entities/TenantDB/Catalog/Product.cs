using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("product")]
    public class Product : EntityBase
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Slug { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        [Required]
        public Guid ProductCategoryId { get; set; }


        public ProductCategory ProductCategory { get; set; }
        public ProductPrice ProductPrice { get; set; }
       // public IEnumerable<CartItem> CartItems { get; set; }
    }
}
