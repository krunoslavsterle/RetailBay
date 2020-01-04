using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly ITenantDBContext _context;

        public GetOrdersQueryHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.ToListAsync();
            return Map(orders);
        }

        private IEnumerable<OrderDTO> Map(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
                yield return new OrderDTO
                {
                    Id = order.Id,
                    OrderStatus = order.OrderStatus,
                    OrderTotal = order.OrderTotal,
                    ShippingAddressId = order.ShippingAddressId,
                    UserId = order.UserId,
                    DateCreated = order.DateCreated
                };
        }
    }
}
