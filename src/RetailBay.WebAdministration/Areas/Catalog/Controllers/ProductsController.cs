using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.SystemDb;
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
        private readonly IAppLogger<ProductsController> _logger;

        #endregion Fields

        #region Constructors

        public ProductsController(ICatalogService catalogService, IAppLogger<ProductsController> logger)
        {
            _catalogService = catalogService;
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

            var dtoList = new List<ProductDTO>();
            foreach (var p in list)
            {
                dtoList.Add(new ProductDTO
                {
                    Id = p.Id,
                    Price = p.ProductPrice.Price,
                    DateCreated = p.DateCreated,
                    Description = p.Description,
                    IsPublished = p.IsPublished,
                    Name = p.Name
                });
            }

            var vm = new ProductsViewModel();
            vm.Products = new PagedCollection<ProductDTO>(dtoList, list.TotalItemCount, list.PageNumber, list.PageSize);
            return View(vm);
        }

        [HttpGet]
        [Route("products/create")]
        public Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View());
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
                    //Price = vm.Price
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
