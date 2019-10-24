using System.Collections.Generic;

namespace RetailBay.WebShop.Models.Home
{
    public class IndexViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
