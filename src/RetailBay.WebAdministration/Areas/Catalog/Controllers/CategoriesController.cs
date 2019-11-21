using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Application.ProductCategories.Commands.DeleteProductCategory;
using RetailBay.Application.ProductCategories.Commands.InsertProductCategory;
using RetailBay.Application.ProductCategories.Commands.UpdateProductCategory;
using RetailBay.Application.ProductCategories.Queries.GetProductCategories;
using RetailBay.Application.ProductCategories.Queries.GetProductCategory;
using System.Threading.Tasks;

namespace RetailBay.WebAdministration.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
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
        public async Task<IActionResult> Edit(UpdateProductCategoryCommand command)
        {
            if (!ModelState.IsValid)
                return await Edit(command.Slug);

            await _mediator.Send(command);
            return RedirectToAction(nameof(CategoriesController.Categories));
        }

        [HttpPost]
        [Route("categories/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteProductCategoryCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(CategoriesController.Categories));
        }
    }
}
