using MediatR;
using System.Collections.Generic;

namespace RetailBay.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {
    }
}
