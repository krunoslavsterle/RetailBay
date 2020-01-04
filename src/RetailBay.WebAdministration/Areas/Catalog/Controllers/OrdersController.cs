using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Application.Orders.Queries.GetOrder;
using RetailBay.Application.Orders.Queries.GetOrders;
using RetailBay.Domain.Entities.TenantDB;
using System;
using System.Threading.Tasks;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Route("orders")]
    [Authorize(Roles = "Administrator")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return View(orders);
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _mediator.Send(new GetOrderQuery(id, nameof(Order.ShippingAddress), nameof(Order.User), $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}"));
            return View(order);
        }
    }
}
