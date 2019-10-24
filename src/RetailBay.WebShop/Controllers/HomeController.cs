using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.SharedKernel.QueryParameters;
using RetailBay.WebShop.Models;
using RetailBay.WebShop.Models.Home;

namespace RetailBay.WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ILookupServiceFactory _lookupServiceFactory;

        public HomeController(ICatalogService catalogService, ILookupServiceFactory lookupServiceFactory, ILogger<HomeController> logger)
        {
            _catalogService = catalogService;
            _lookupServiceFactory = lookupServiceFactory;
        }

        public async Task<IActionResult> Index(string categorySlug)
        {   
            var sortingParameters = new SortingParameters();
            sortingParameters.Add("Id", false);

            Expression<Func<Product, bool>> predicate = categorySlug == null ? (Expression<Func<Product, bool>>)null : o => o.ProductCategory.Slug == categorySlug;
            
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

            var categories = await _lookupServiceFactory.Create<ProductCategory>().GetAllAsync();
            var products = await _catalogService.GetProductsPagedAsync(p => p.Slug == productSlug, null, 1, 1);

            var vm = new ProductViewModel
            {
                Categories = Mapper.Map(categories).ToANew<IEnumerable<CategoryDTO>>(),
                Product = Mapper.Map(products.Items.First()).ToANew<ProductDTO>()
            };

            return View(vm);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
