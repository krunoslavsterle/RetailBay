using System;

namespace RetailBay.WebAdministration.Areas.Catalog.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
