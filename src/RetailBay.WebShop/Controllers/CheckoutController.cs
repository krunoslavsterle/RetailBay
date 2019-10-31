using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.WebShop.Models.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailBay.WebShop.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutController" /> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        /// <param name="cartService">The cart service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="userManager">The user manager.</param>
        public CheckoutController(IOrderService orderService, ICartService cartService, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var userId = new Guid(_userManager.GetUserId(User));
            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);

            var cartTask = _cartService.GetCartAsync(cartId, $"{nameof(Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            var shippingAddressesTask = _userService.GetAddressesForUserAsync(userId, AddressType.Shipping);
            await Task.WhenAll(cartTask, shippingAddressesTask);

            var cart = await cartTask;
            if (cart == null || cart.UserId != userId)
                return RedirectToAction("Index", "Home");

            var vm = new IndexViewModel
            {
                ShippingAddresses = await shippingAddressesTask,
                CartItems = Mapper.Map(cart.CartItems).ToANew<IEnumerable<Models.Cart.CartItemDTO>>(),
                ShippingPrice = 20
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(IndexViewModel vm)
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
            var userId = new Guid(_userManager.GetUserId(User));

            await _orderService.CreateOrderForUserAsync(userId, cartId, vm.SelectedAddressId);

            return RedirectToAction("Index", "Home");
        }
    }
}
