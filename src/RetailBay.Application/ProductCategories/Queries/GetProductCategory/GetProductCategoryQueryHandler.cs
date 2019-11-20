using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.ProductCategories.Queries.GetProductCategory
{
    public class GetProductCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, ProductCategoryDTO>
    {
        private readonly ITenantDBContext _context;

        public GetProductCategoryQueryHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<ProductCategoryDTO> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProductCategories.AsQueryable();
            if (request.Id.HasValue)
                query = query.Where(p => p.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.Slug))
                query = query.Where(p => p.Slug == request.Slug);

            var entity = await query.FirstOrDefaultAsync();
            return Map(entity);
        }

        private ProductCategoryDTO Map(ProductCategory productCategory)
        {
            if (productCategory == null)
                return null;

            return new ProductCategoryDTO
            {
                Id = productCategory.Id,
                Abrv = productCategory.Abrv,
                Slug = productCategory.Slug,
                DateCreated = productCategory.DateCreated,
                DateUpdated = productCategory.DateUpdated,
                IsDeleted = productCategory.IsDeleted,
                Name = productCategory.Name
            };
        }
    }
}
