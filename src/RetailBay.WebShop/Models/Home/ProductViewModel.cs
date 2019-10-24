using System.Collections.Generic;

namespace RetailBay.WebShop.Models.Home
{
    public class ProductViewModel
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public ProductDTO Product { get; set; }
    }
}
