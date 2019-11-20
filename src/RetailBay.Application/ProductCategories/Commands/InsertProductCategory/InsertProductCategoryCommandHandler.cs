using MediatR;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.ProductCategories.Commands.InsertProductCategory
{
    public class InsertProductCategoryCommandHandler : IRequestHandler<InsertProductCategoryCommand, int>
    {
        private readonly ITenantDBContext _context;

        public InsertProductCategoryCommandHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public Task<int> Handle(InsertProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Abrv = request.Abrv,
                Name = request.Name,
                Slug = request.Slug,
                IsDeleted = false,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _context.ProductCategories.Add(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
