using System;

namespace RetailBay.Application.Products.Queries.GetEditProductVM
{
    public class EditProductDTO
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
