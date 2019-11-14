using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Interfaces;
using RetailBay.Domain.Entities.Identity;
using RetailBay.Domain.Entities.TenantDB;
using RetailBay.WebShop.Common;
using RetailBay.WebShop.Models.Cart;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailBay.WebShop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;
        private readonly ILookupServiceFactory _lookupServiceFactory;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CartController" /> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="lookupServiceFactory">The lookup service factory.</param>
        public CartController(ICartService cartService, ILookupServiceFactory lookupServiceFactory, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _lookupServiceFactory = lookupServiceFactory;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return View(null);

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
            
            var categoriesTask = _lookupServiceFactory.Create<ProductCategory>().GetAllAsync();
            var cartTask = _cartService.GetCartAsync(cartId, $"{nameof(Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            await Task.WhenAll(categoriesTask, cartTask);

            var categories = await categoriesTask;
            var cart = await cartTask;

            var vm = new IndexViewModel
            {
                Categories = Mapper.Map(categories).ToANew<IEnumerable<Models.Home.CategoryDTO>>()
            };

            if (cart != null)
                vm.Products = Mapper.Map(cart.CartItems).ToANew<IEnumerable<Models.Cart.CartItemDTO>>();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var cartId = GetOrSetCartCookie();
            var cartExists = await _cartService.CheckCartExistsAsync(cartId);
            if (!cartExists)
            {
                var userId = (Guid?)null;
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    userId = user.Id;
                }

                await _cartService.CreateCartForUserAsync(userId, cartId);
            }

            var productsCount = await _cartService.AddProductToCartAsync(cartId, productId);
            return new JsonResult(productsCount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            if (Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                await _cartService.RemoveCartItemAsync(id);

            return RedirectToAction("Index");
        }

        private Guid GetOrSetCartCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);

            var anonymousId = Guid.NewGuid();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Today.AddDays(30)
            };

            Response.Cookies.Append(Constants.CART_COOKIE_NAME, anonymousId.ToString(), cookieOptions);
            return anonymousId;
        }
    }
}
