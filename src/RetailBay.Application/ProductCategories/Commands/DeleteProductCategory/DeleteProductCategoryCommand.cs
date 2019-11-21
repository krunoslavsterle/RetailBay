using MediatR;
using System;

namespace RetailBay.Application.ProductCategories.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
}
