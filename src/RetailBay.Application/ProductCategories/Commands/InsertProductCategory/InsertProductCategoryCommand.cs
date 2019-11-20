using MediatR;

namespace RetailBay.Application.ProductCategories.Commands.InsertProductCategory
{
    public class InsertProductCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string Slug { get; set; }
    }
}
