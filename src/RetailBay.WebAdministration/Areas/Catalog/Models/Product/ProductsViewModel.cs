using System;
using System.Collections.Generic;
using RetailBay.Core.SharedKernel.Collections;

namespace RetailBay.WebAdministration.Areas.Catalog.Models
{
    public class ProductsViewModel
    {
        public IPagedCollection<ProductDTO> Products { get; set; }
        public IDictionary<Guid, string> Categories { get; set; }

        public struct ProductDTO
        {
            public Guid Id { get; set; }
            public Guid ProductCategoryId { get; set; }
            public string Name { get; set; }
            public string Slug { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public bool IsPublished { get; set; }
            public DateTime DateCreated { get; set; }
        }
    }
}
