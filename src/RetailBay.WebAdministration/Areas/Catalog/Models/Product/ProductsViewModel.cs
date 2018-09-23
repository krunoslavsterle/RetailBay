using System;
using RetailBay.Core.SharedKernel.Collections;

namespace RetailBay.WebAdministration.Areas.Catalog.Models
{
    public class ProductsViewModel
    {
        public IPagedCollection<ProductDTO> Products { get; set; }

        public struct ProductDTO
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public bool IsPublished { get; set; }
            public DateTime DateCreated { get; set; }
        }
    }
}
