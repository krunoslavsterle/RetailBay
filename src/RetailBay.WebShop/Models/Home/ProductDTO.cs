using System;

namespace RetailBay.WebShop.Models.Home
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public PriceDTO ProductPrice { get; set; }
    }
}
