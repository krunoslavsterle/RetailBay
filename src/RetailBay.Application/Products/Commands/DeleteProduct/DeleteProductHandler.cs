using MediatR;
using RetailBay.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly ITenantDBContext _context;

        public DeleteProductHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(request.Id);

            _context.Products.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
