using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailBay.Application.Common.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailBay.Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDTO>
    {
        private readonly ITenantDBContext _context;

        public GetOrderQueryHandler(ITenantDBContext tenantDBContext)
        {
            _context = tenantDBContext;
        }

        public async Task<OrderDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .Where(p => p.Id == request.Id);

            foreach (var param in request.IncludeParameters)
                query = query.Include(param);

            var order = await query.FirstOrDefaultAsync();
            return Map(order);
        }

        private OrderDTO Map(Order order)
        {
            var shipingAddress = new ShippingDTO
            {
                City = order.ShippingAddress.City,
                ContactName = order.ShippingAddress.ContactName,
                Country = order.ShippingAddress.Country,
                Phone = order.ShippingAddress.Phone,
                PostalCode = order.ShippingAddress.PostalCode,
                StreetAddress = order.ShippingAddress.StreetAddress
            };

            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                DateCreated = order.DateCreated,
                OrderStatus = order.OrderStatus,
                OrderTotal = order.OrderTotal,
                ShippingAddressId = order.ShippingAddressId,
                UserId = order.UserId,
                ShippingAddress = shipingAddress
            };

            foreach(var item in order.OrderItems)
                orderDTO.OrderItems.Add(new OrderItemDTO
                {
                    Product = item.Product.Name,
                    ProductId = item.ProductId,
                    ProductPrice = item.ProductPrice
                });

            return orderDTO;
        }
    }
}
