using MediatR;
using System.Collections.Generic;

namespace RetailBay.Application.ProductCategories.Queries.GetProductCategories
{
    public class GetProductCategoriesQuery : IRequest<IEnumerable<ProductCategoryDTO>>
    {
    }
}
