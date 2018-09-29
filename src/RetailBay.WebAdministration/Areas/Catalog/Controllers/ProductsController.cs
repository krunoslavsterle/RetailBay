using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var dtoList = new List<ProductDTO>();
            foreach (var p in list)
            {
                dtoList.Add(new ProductDTO
                {
                    Id = p.Id,
                    ProductCategoryId = p.ProductCategoryId,
                    Name = p.Name,
                    Slug = p.Slug,
                    Price = p.ProductPrice.Price,
                    IsPublished = p.IsPublished,
                    DateCreated = p.DateCreated,
                    Description = p.Description
                });
            }

            var vm = new ProductsViewModel
            {
                Products = new PagedCollection<ProductDTO>(dtoList, list.TotalItemCount, list.PageNumber, list.PageSize),
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
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Description = vm.Description,
                    IsPublished = vm.IsPublished,
                    Name = vm.Name,
                    Slug = vm.Slug,
                    ProductCategoryId = vm.ProductCategoryId
                };

                product.ProductPrice = new ProductPrice
                {
                    Id = Guid.NewGuid(),
                    Price = vm.Price,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                await _catalogService.CreateProductAsync(product);
            }

            return RedirectToAction(nameof(ProductsController.Products));
        }

        [HttpGet]
        [Route("products/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _catalogService.GetProductAsync(id);
            var vm = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IsPublished = product.IsPublished,
                //Price = product.Price
            };

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
                var product = new Product
                {
                    Id = vm.Id,
                    Description = vm.Description,
                    IsPublished = vm.IsPublished,
                    Name = vm.Name,
                    //Price = vm.Price
                };


                await _catalogService.EditProductAsync(product);
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
