using MediatR;
using System;

namespace RetailBay.Application.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string Slug { get; set; }
    }
}
