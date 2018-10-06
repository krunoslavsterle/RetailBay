using System;
using System.Collections.Generic;

namespace RetailBay.WebShop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }

        public class ProductDTO
        {
            public Guid Id { get; set; }
            public Guid ProductCategoryId { get; set; }
            public string Name { get; set; }
            public string Slug { get; set; }
            public string Description { get; set; }
            public PriceDTO ProductPrice { get; set; }
        }

        public class PriceDTO
        {
            public decimal Price { get; set; }
        }
    }
}
