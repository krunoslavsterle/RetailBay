using MediatR;
using RetailBay.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, int>
    {
        private readonly ITenantDBContext _context;
        
        public UpdateProductCategoryCommandHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<int> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.ProductCategories.FindAsync(request.Id);
            category.Name = request.Name;
            category.Slug = request.Slug;
            category.Abrv = request.Abrv;
            category.DateUpdated = DateTime.UtcNow;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
