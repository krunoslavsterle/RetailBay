using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Common.Collections;
using RetailBay.Domain.Entities.TenantDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Products.Queries.GetProductsPagedList
{
    public class GetProductsPagedListHandler : IRequestHandler<GetProductsPagedListQuery, ProductsVM>
    {
        private readonly ITenantDBContext _context;
        
        public GetProductsPagedListHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        // Inject Mapper

        public async Task<ProductsVM> Handle(GetProductsPagedListQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Products.CountAsync();
            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip(request.PageNumber)
                .Take(request.PageSize)
                .Include(p => p.ProductPrice)
                .ToListAsync();

            var categories = await _context.ProductCategories.ToListAsync();

            var vm = new ProductsVM
            {
                Products = new PagedCollection<ProductDTO>(Map(products), totalCount, request.PageNumber, request.PageSize),
                Categories = categories.ToDictionary(key => key.Id, value => value.Name)
            };

            return vm;
        }

        private IEnumerable<ProductDTO> Map(IEnumerable<Product> products)
        {
            foreach (var product in products)
                yield return new ProductDTO
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
