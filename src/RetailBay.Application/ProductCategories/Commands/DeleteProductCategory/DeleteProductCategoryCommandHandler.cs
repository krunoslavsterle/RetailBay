using MediatR;
using RetailBay.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.ProductCategories.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, int>
    {
        private readonly ITenantDBContext _context;

        public DeleteProductCategoryCommandHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<int> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.ProductCategories.FindAsync(request.Id);
            _context.ProductCategories.Remove(category);
         
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
