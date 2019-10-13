using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.WebAdministration.Areas.Catalog.Models;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private readonly ILookupServiceFactory _lookupServiceFactory;
        private readonly IAppLogger<ProductsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="lookupServiceFactory">The lookup service factory.</param>
        /// <param name="logger">The logger.</param>
        public CategoriesController(ILookupServiceFactory lookupServiceFactory, IAppLogger<ProductsController> logger)
        {
            _lookupServiceFactory = lookupServiceFactory;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> Categories()
        {
            var lkpService = _lookupServiceFactory.Create<ProductCategory>();
            var productCategories = await lkpService.GetAllAsync();

            var vm = new ProductCategoriesViewModel()
            {
                ProductCategories = Mapper.Map(productCategories).ToANew<IEnumerable<ProductCategoriesViewModel.ProductCategoryDTO>>()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("categories/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("categories/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newCategory = Mapper.Map(vm).ToANew<ProductCategory>();
                newCategory.Id = Guid.NewGuid();

                var lkpService = _lookupServiceFactory.Create<ProductCategory>();
                await lkpService.InsertAsync(newCategory);
            }

            return RedirectToAction(nameof(CategoriesController.Categories));
        }

        [HttpGet]
        [Route("categories/edit")]
        public async Task<IActionResult> Edit(string slug)
        {
            var lkpService = _lookupServiceFactory.Create<ProductCategory>();
            var category = await lkpService.GetOneBySlugAsync(slug);

            var vm = Mapper.Map(category).ToANew<EditProductCategoryViewModel>();
            return View(vm);
        }

        [HttpPost]
        [Route("categories/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var lkpService = _lookupServiceFactory.Create<ProductCategory>();
                var category = await lkpService.GetOneById(vm.Id);
                Mapper.Map(vm).Over(category);

                await lkpService.UpdateAsync(category);
            }

            return RedirectToAction(nameof(CategoriesController.Edit), new { slug = vm.Slug });
        }

        [HttpPost]
        [Route("categories/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var lkpService = _lookupServiceFactory.Create<ProductCategory>();
            await lkpService.DeleteAsync(id);
            return RedirectToAction(nameof(CategoriesController.Categories));
        }
    }
}
