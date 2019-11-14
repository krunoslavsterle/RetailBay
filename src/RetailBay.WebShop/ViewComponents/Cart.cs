using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Interfaces;
using RetailBay.WebShop.Common;
using System;
using System.Threading.Tasks;

namespace RetailBay.WebShop.ViewComponents
{
    /// <summary>
    /// Cart View Component.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class Cart : ViewComponent
    {
        private readonly ICartService _cartService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cart" /> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        public Cart(ICartService cartService)
        {
            _cartService = cartService;
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
                count = await _cartService.GetNumberOfProductsInCartAsync(new Guid(cartId));
            }

            return View(count);
        }
    }
}
