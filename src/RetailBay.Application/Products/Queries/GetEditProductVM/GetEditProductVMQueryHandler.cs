using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Products.Queries.GetEditProductVM
{
    public class GetEditProductVMQueryHandler : IRequestHandler<GetEditProductVMQuery, EditProductVM>
    {
        private readonly ITenantDBContext _context;

        public GetEditProductVMQueryHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<EditProductVM> Handle(GetEditProductVMQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.Id == request.Id)
                .Include(p => p.ProductPrice)
                .FirstOrDefaultAsync();

            var categories = await _context.ProductCategories.ToListAsync();

            return new EditProductVM
            {
                Product = Map(product),
                Categories = categories.ToDictionary(key => key.Id, value => value.Name)
            };
        }

        private EditProductDTO Map(Product product)
        {
            return new EditProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                IsPublished = product.IsPublished,
                Description = product.Description,
                Price = product.ProductPrice.Price,
                ProductCategoryId = product.ProductCategoryId,
                DateCreated = product.DateCreated,
                Slug = product.Slug
            };
        }
    }
}
