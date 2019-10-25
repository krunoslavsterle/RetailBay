using RetailBay.WebShop.Models.Home;
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
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
