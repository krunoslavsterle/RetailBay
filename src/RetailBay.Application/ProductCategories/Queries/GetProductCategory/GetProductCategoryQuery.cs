using MediatR;
using System;

namespace RetailBay.Application.ProductCategories.Queries.GetProductCategory
{
    public class GetProductCategoryQuery : IRequest<ProductCategoryDTO>
    {
        public Guid? Id { get; set; }
        public string Slug { get; set; }
    }
}
