using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.SharedKernel.Collections;
using RetailBay.Core.SharedKernel.QueryParameters;
using RetailBay.WebAdministration.Areas.Catalog.Models;
using static RetailBay.WebAdministration.Areas.Catalog.Models.ProductsViewModel;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
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
        public ProductsController(ICatalogService catalogService, ILookupServiceFactory lookupServiceFactory, IAppLogger<ProductsController> logger)
        {
            _catalogService = catalogService;
            _lookupServiceFactory = lookupServiceFactory;
            _logger = logger;
        }

        #endregion Constructors

        #region Actions

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> Products(int pageNumber = 1, int pageSize = 10, string orderBy = "", bool isAscending = true)
        {
            _logger.LogInformation("Request received for [{action}] GET action", nameof(ProductsController.Products));

            if (string.IsNullOrWhiteSpace(orderBy))
                orderBy = nameof(Product.DateCreated);
            
            var sortingParameters = new SortingParameters();
            sortingParameters.Add(orderBy, isAscending);
            var list = await _catalogService.GetProductsPagedAsync(sortingParameters, pageNumber, pageSize);

            var lkpCategories = _lookupServiceFactory.Create<ProductCategory>();
            var productCategories = await lkpCategories.GetAllAsync();
                        
            var vm = new ProductsViewModel
            {
                Products = new PagedCollection<ProductDTO>(Mapper.Map(list).ToANew<IEnumerable<ProductDTO>>(), list.TotalItemCount, list.PageNumber, list.PageSize),
                Categories = productCategories.ToDictionary(key => key.Id, value => value.Name)
            };

            return View(vm);
        }

        [HttpGet]
        [Route("products/create")]
        public async Task<IActionResult> Create()
        {
            var lkpCategories = _lookupServiceFactory.Create<ProductCategory>();
            var productCategories = await lkpCategories.GetAllAsync();

            ViewBag.ProductCategories = new SelectList(productCategories, nameof(ProductCategory.Id), nameof(ProductCategory.Name));
            return View();
        }

        [HttpPost]
        [Route("products/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            if (ModelState.IsValid)
            {
                var newProduct = Mapper.Map(vm).ToANew<Product>();
                newProduct.Id = Guid.NewGuid();

                newProduct.ProductPrice = new Product
                {
                    Id = Guid.NewGuid(),
                    ProductId = newProduct.Id,
                    Price = vm.Price,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                await _catalogService.CreateProductAsync(newProduct);
            }

            return RedirectToAction(nameof(ProductsController.Products));
        }

        [HttpGet]
        [Route("products/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _catalogService.GetProductAsync(id);
            var vm = Mapper.Map(product).ToANew<EditProductViewModel>();
            vm.Price = product.ProductPrice.Price;

            var lkpCategories = _lookupServiceFactory.Create<ProductCategory>();
            var productCategories = await lkpCategories.GetAllAsync();
            ViewBag.ProductCategories = new SelectList(productCategories, nameof(ProductCategory.Id), nameof(ProductCategory.Name));
            return View(vm);
        }

        [HttpPost]
        [Route("products/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            if (ModelState.IsValid)
            {
                var domain = await _catalogService.GetProductAsync(vm.Id);
                Mapper.Map(vm).Over(domain);

                await _catalogService.EditProductAsync(domain);
            }

            return RedirectToAction(nameof(ProductsController.Edit), new { id = vm.Id });
        }

        [HttpPost]
        [Route("products/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _catalogService.DeleteProductAsync(id);
            return RedirectToAction(nameof(ProductsController.Products));
        }

        #endregion Actions
    }
}
