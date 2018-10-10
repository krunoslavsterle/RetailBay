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

        private readonly ICatalogService _catalogService;
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
        public OrdersController(ICatalogService catalogService, ILookupServiceFactory lookupServiceFactory, IAppLogger<ProductsController> logger)
        {
            _catalogService = catalogService;
            _lookupServiceFactory = lookupServiceFactory;
            _logger = logger;
        }

        #endregion Constructors

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Request received for [{action}] GET action", nameof(OrdersController.Index));

            var orders = await _catalogService.GetAllOrdersAsync();
            return View(orders);
        }
    }
}
