using AgileObjects.AgileMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Application.ProductCategories.Commands.InsertProductCategory;
using RetailBay.Application.ProductCategories.Queries.GetProductCategories;
using RetailBay.Application.ProductCategories.Queries.GetProductCategory;
using RetailBay.Core.Interfaces;
using RetailBay.Domain.Entities.TenantDB;
using RetailBay.WebAdministration.Areas.Catalog.Models;
using System;
using System.Threading.Tasks;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private readonly ILookupServiceFactory _lookupServiceFactory;
        private readonly IMediator _mediator;


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="lookupServiceFactory">The lookup service factory.</param>
        public CategoriesController(ILookupServiceFactory lookupServiceFactory, IMediator mediator)
        {
            _lookupServiceFactory = lookupServiceFactory;
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> Categories()
        {
            return View(await _mediator.Send(new GetProductCategoriesQuery()));
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
        public async Task<IActionResult> Create(InsertProductCategoryCommand command)
        {
            if (!ModelState.IsValid)
                return Create();

            await _mediator.Send(command);
            return RedirectToAction(nameof(CategoriesController.Categories));
        }

        [HttpGet]
        [Route("categories/edit")]
        public async Task<IActionResult> Edit(string slug)
        {
            return View(await _mediator.Send(new GetProductCategoryQuery { Slug = slug }));
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
