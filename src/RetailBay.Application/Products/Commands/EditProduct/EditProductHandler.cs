using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Products.Commands.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, int>
    {
        private readonly ITenantDBContext _context;

        public EditProductHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<int> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products
                .Where(p => p.Id == request.Id)
                .Include(p => p.ProductPrice)
                .FirstAsync();
            
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.IsPublished = request.IsPublished;
            entity.ProductCategoryId = request.ProductCategoryId;
            entity.ProductPrice.Price = request.Price;
            entity.Slug = request.Slug;
            entity.DateUpdated = DateTime.UtcNow;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
