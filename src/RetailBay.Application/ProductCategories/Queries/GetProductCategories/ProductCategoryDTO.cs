using System;

namespace RetailBay.Application.ProductCategories.Queries.GetProductCategories
{
    public class ProductCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; }
    }
}
