using System;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.WebAdministration.Areas.Catalog.Models.Order;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Route("orders")]
    [Authorize(Roles = "Administrator")]
    public class OrdersController : Controller
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly ILookupServiceFactory _lookupServiceFactory;
        private readonly IAppLogger<ProductsController> _logger;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="catalogService">The catalog service.</param>
        /// <param name="lookupService">The lookup service.</param>
        /// <param name="logger">The logger.</param>
        public OrdersController(IOrderService orderService, ILookupServiceFactory lookupServiceFactory, IAppLogger<ProductsController> logger)
        {
            _orderService = orderService;
            _lookupServiceFactory = lookupServiceFactory;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(Guid id)
        {
            var vm = new DetailsViewModel();
            var order = await _orderService.GetOrderAsync(id, nameof(Order.ShippingAddress), nameof(Order.OrderItems));

            vm.Order = Mapper.Map(order).ToANew<DetailsViewModel.OrderDTO>();
            return View(vm);
        }

        #endregion Methods
    }
}
