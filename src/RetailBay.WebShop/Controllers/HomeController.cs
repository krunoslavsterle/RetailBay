using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.SharedKernel.QueryParameters;
using RetailBay.WebShop.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailBay.WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ILookupServiceFactory _lookupServiceFactory;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="catalogService">The catalog service.</param>
        /// <param name="lookupServiceFactory">The lookup service factory.</param>
        /// <param name="logger">The logger.</param>
        public HomeController(ICatalogService catalogService, ILookupServiceFactory lookupServiceFactory, ILogger<HomeController> logger)
        {
            _catalogService = catalogService;
            _lookupServiceFactory = lookupServiceFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string categorySlug)
        {   
            var sortingParameters = new SortingParameters();
            sortingParameters.Add("Id", false);

            Expression<Func<Product, bool>> predicate = o => (categorySlug == null) || o.ProductCategory.Slug == categorySlug;
            var products = _catalogService.GetProductsPagedAsync(predicate, sortingParameters, 1, 10);
            var categories = _lookupServiceFactory.Create<ProductCategory>().GetAllAsync();
            await Task.WhenAll(products, categories);
           
            var vm = new IndexViewModel
            {
                Products = Mapper.Map(await products).ToANew<IEnumerable<ProductDTO>>(),
                Categories = Mapper.Map(await categories).ToANew<IEnumerable<CategoryDTO>>()
            };

            return View(vm);
        }

        public async Task<IActionResult> Product(string productSlug)
        {
            if (string.IsNullOrWhiteSpace(productSlug))
                throw new ArgumentException(nameof(productSlug));

            var categories = _lookupServiceFactory.Create<ProductCategory>().GetAllAsync();
            var product = _catalogService.GetProductBySlugAsync(productSlug);
            await Task.WhenAll(categories, product);

            var vm = new ProductViewModel
            {
                Categories = Mapper.Map(await categories).ToANew<IEnumerable<CategoryDTO>>(),
                Product = Mapper.Map(await product).ToANew<ProductDTO>()
            };

            return View(vm);
        }        
    }
}
