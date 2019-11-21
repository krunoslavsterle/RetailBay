using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailBay.Application.ProductCategories.Queries;
using RetailBay.Application.ProductCategories.Queries.GetProductCategories;
using RetailBay.Application.Products.Commands.DeleteProduct;
using RetailBay.Application.Products.Commands.EditProduct;
using RetailBay.Application.Products.Commands.InsertProduct;
using RetailBay.Application.Products.Queries.GetEditProductVM;
using RetailBay.Application.Products.Queries.GetProductsPagedList;
using System;
using System.Threading.Tasks;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> Products(int pageNumber = 1, int pageSize = 10, string orderBy = "", bool isAscending = true)
        {
            return View(await _mediator.Send(new GetProductsPagedListQuery(pageNumber, pageSize)));
        }

        [HttpGet]
        [Route("products/create")]
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetProductCategoriesQuery());
            ViewBag.ProductCategories = new SelectList(categories, nameof(ProductCategoryDTO.Id), nameof(ProductCategoryDTO.Name));
            return View();
        }

        [HttpPost]
        [Route("products/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertProductCommand command)
        {
            if (!ModelState.IsValid)
                return await Create();
            
            await _mediator.Send(command);
            return RedirectToAction(nameof(ProductsController.Products));
        }

        [HttpGet]
        [Route("products/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var vm = await _mediator.Send(new GetEditProductVMQuery(id));
            
            ViewBag.ProductCategories = new SelectList(vm.Categories, "Key", "Value");
            return View(vm.Product);
        }

        [HttpPost]
        [Route("products/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductCommand command)
        {
            if (!ModelState.IsValid)
                return await Edit(command.Id);
             
            await _mediator.Send(command);
            return RedirectToAction(nameof(ProductsController.Edit), new { id = command.Id });
        }

        [HttpPost]
        [Route("products/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(ProductsController.Products));
        }
    }
}
