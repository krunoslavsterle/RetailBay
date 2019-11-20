using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.ProductCategories.Queries.GetProductCategories
{
    public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, IEnumerable<ProductCategoryDTO>>
    {
        private readonly ITenantDBContext _context;

        public GetProductCategoriesQueryHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<IEnumerable<ProductCategoryDTO>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.ProductCategories.ToListAsync();
            return Map(categories);
        }

        private IEnumerable<ProductCategoryDTO> Map(IEnumerable<ProductCategory> productCategories)
        {
            foreach (var category in productCategories)
                yield return new ProductCategoryDTO
                {
                    Id = category.Id,
                    Abrv = category.Abrv,
                    Name = category.Name,
                    Slug = category.Slug,
                    IsDeleted = category.IsDeleted,
                    DateCreated = category.DateCreated,
                    DateUpdated = category.DateUpdated
                };
        }
    }
}
