using MediatR;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Products.Commands.InsertProduct
{
    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand, int>
    {
        private readonly ITenantDBContext _context;

        public InsertProductCommandHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public Task<int> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var productId = Guid.NewGuid();
            var entity = new Product
            {
                Id = productId,
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ProductCategoryId = request.ProductCategoryId,
                IsPublished = request.IsPublished,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ProductPrice = new ProductPrice
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Price = request.Price,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                }
            };

            _context.Products.Add(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
