using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailBay.Core.Interfaces;
using RetailBay.Core.SharedKernel.QueryParameters;
using RetailBay.WebShop.Models;

namespace RetailBay.WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ICatalogService catalogService, ILogger<HomeController> logger)
        {
            _catalogService = catalogService;

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            _logger.Log(LogLevel.Warning, "This is info");

            
            var sortingParameters = new SortingParameters();
            sortingParameters.Add("Id", false);

            var list = await _catalogService.GetProductsPagedAsync(sortingParameters, 1, 10);
            var vm = new IndexViewModel();
            vm.Products = Mapper.Map(list).ToANew<IEnumerable<IndexViewModel.ProductDTO>>();
            
            return View(vm);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
