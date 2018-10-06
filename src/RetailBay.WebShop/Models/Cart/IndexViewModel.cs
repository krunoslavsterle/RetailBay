using System;
using System.Collections.Generic;

namespace RetailBay.WebShop.Models.Cart
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Products = new List<CartItemDTO>();
        }

        public IEnumerable<CartItemDTO> Products { get; set; }

        public struct CartItemDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }
}
