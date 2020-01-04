using MediatR;
using System;

namespace RetailBay.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDTO>
    {
        public GetOrderQuery(Guid id, params string[] includeParameters)
        {
            Id = id;
            IncludeParameters = includeParameters;
        }

        public Guid Id { get; set; }
        public string[] IncludeParameters { get; set; }
    }
}
