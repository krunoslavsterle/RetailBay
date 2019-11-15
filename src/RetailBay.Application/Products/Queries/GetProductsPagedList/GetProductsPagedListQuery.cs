using MediatR;

namespace RetailBay.Application.Products.Queries.GetProductsPagedList
{
    public class GetProductsPagedListQuery : IRequest<ProductsVM>
    {
        public GetProductsPagedListQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
