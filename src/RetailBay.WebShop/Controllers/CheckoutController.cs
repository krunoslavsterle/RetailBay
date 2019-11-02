using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
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
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IShippingAddressService _shippingAddressService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutController" /> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        /// <param name="cartService">The cart service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="shippingAddressService">The shipping address service.</param>
        /// <param name="userManager">The user manager.</param>
        public CheckoutController(IOrderService orderService, ICartService cartService, IUserService userService, IShippingAddressService shippingAddressService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userService = userService;
            _userManager = userManager;
            _shippingAddressService = shippingAddressService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);

            var cartTask = _cartService.GetCartAsync(cartId, $"{nameof(Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            var cartShippingAddressTask = _shippingAddressService.GetShippingAddressForCartAsync(cartId);
            
            await Task.WhenAll(cartTask, cartShippingAddressTask);

            var cart = await cartTask;
            if (cart == null)
                return RedirectToAction("Index", "Home");

            var vm = new IndexViewModel
            {
                ShippingAddress = await cartShippingAddressTask,
                CartItems = Mapper.Map(cart.CartItems).ToANew<IEnumerable<Models.Cart.CartItemDTO>>(),
                ShippingPrice = 20
            };
            return View(vm);
        }

        [HttpGet("addaddress")]
        public async Task<IActionResult> AddAddress()
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
            var cart = await _cartService.GetCartAsync(cartId, $"{nameof(Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            if (cart == null)
                return RedirectToAction("Index", "Home");

            var vm = new IndexViewModel
            {
                CartItems = Mapper.Map(cart.CartItems).ToANew<IEnumerable<Models.Cart.CartItemDTO>>(),
                ShippingPrice = 20
            };

            return View(vm);
        }

        [HttpPost("addaddress")]
        public async Task<IActionResult> AddAddress(NewAddressDTO dto)
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);

            if (ModelState.IsValid)
            {
                var address = Mapper.Map(dto).ToANew<Address>();
                address.ContactName = $"{dto.FirstName} {dto.LastName}";
                await _shippingAddressService.InsertShippingAddressForCartAsync(address, cartId);
                return RedirectToAction("Index");
            }

            return RedirectToAction("AddAddress");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder()
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
            var userId = (Guid?)null;
            if (User.Identity.IsAuthenticated)
                userId = new Guid(_userManager.GetUserId(User));

            await _orderService.CreateOrderForUserAsync(userId, cartId);
            return RedirectToAction("Index", "Home");
        }
    }
}
