using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces;

namespace RetailBay.WebShop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICatalogService _catalogService;

        public CartController(ICatalogService catalogService, UserManager<ApplicationUser> userManager)
        {
            _catalogService = catalogService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var cartId = GetOrSetCartCookie();
            var cartExists = await _catalogService.CheckCartExists(cartId);
            if (!cartExists)
            {
                var userId = (Guid?)null;
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    userId = user.Id;
                }

                await _catalogService.CreateCartForUser(userId, cartId);
            }

            var productsCount = await _catalogService.AddProductToCart(cartId, productId);
            return new JsonResult(productsCount);
        }

        private Guid GetOrSetCartCookie()
        {
            if (Request.Cookies.ContainsKey("RetailBay.Cart"))
                return new Guid(Request.Cookies["RetailBay.Cart"]);

            var anonymousId = Guid.NewGuid();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Today.AddDays(30)
            };

            Response.Cookies.Append("RetailBay.Cart", anonymousId.ToString(), cookieOptions);
            return anonymousId;
        }
    }
}
