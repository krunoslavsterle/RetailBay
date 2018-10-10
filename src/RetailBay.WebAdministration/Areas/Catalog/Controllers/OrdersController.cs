using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Interfaces;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
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
        [Route("orders")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Request received for [{action}] GET action", nameof(OrdersController.Index));

            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        #endregion Methods
    }
}
