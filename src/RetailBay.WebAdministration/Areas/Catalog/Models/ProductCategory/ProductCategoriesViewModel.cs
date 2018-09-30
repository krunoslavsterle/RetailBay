using System;
using System.Collections.Generic;

namespace RetailBay.WebAdministration.Areas.Catalog.Models
{
    public class ProductCategoriesViewModel
    {
        public IEnumerable<ProductCategoryDTO> ProductCategories { get; set; }

        public struct ProductCategoryDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Abrv { get; set; }
            public string Slug { get; set; }
            public bool IsDeleted { get; set; }
            public DateTime DateCreated { get; set; }
        }
    }
}
