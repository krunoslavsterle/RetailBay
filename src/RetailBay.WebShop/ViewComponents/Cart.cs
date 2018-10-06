using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core;
using RetailBay.Core.Interfaces;

namespace RetailBay.WebShop.ViewComponents
{
    /// <summary>
    /// Cart View Component.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class Cart : ViewComponent
    {
        private readonly ICatalogService _catalogService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Cart"/> class.
        /// </summary>
        /// <param name="catalogService">The catalog service.</param>
        public Cart(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        /// <summary>
        /// Invokes the ViewComponent asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int count = 0;
            if (Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
            {
                var cartId = Request.Cookies[Constants.CART_COOKIE_NAME];
                count = await _catalogService.GetNumberOfProductsInCartAsync(new Guid(cartId));
            }

            return View(count);
        }
    }
}
