using MediatR;
using System;

namespace RetailBay.Application.Products.Commands.EditProduct
{
    public class UpdateProductCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public decimal Price { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
