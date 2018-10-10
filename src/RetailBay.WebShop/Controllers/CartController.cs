using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.WebShop.Models.Cart;

namespace RetailBay.WebShop.Controllers
{
    public class CartController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;

        #endregion Fields

        #region Controllers

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController" /> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        /// <param name="userManager">The user manager.</param>
        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        #endregion Controllers

        #region Methods

        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            var vm = new IndexViewModel();

            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return View(vm);

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
            var cart = await _cartService.GetCartAsync(cartId, $"{nameof(Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            if (cart == null)
                return View(vm);

            vm.Products = MapDomainCartItemsToProductDTO(cart.CartItems);

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

        private IEnumerable<IndexViewModel.CartItemDTO> MapDomainCartItemsToProductDTO(IEnumerable<CartItem> cartItems)
        {
            foreach (var cartItem in cartItems)
                yield return new IndexViewModel.CartItemDTO { Name = cartItem.Product.Name, Price = cartItem.Product.ProductPrice.Price, Id = cartItem.Id };
        }

        #endregion Methods
    }
}
