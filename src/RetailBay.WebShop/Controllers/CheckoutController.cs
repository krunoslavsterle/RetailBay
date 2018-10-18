using System;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces;
using RetailBay.WebShop.Models.Checkout;

namespace RetailBay.WebShop.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        #endregion Fields

        #region Controllers

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController" /> class.
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

        #endregion Controllers

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                return RedirectToAction("Index", "Home");

            var userId = new Guid(_userManager.GetUserId(User));
            var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);

            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null || cart.UserId != userId)
                return RedirectToAction("Index", "Home");

            var vm = new IndexViewModel();
            vm.ShippingAddresses = await _userService.GetAddressesForUserAsync(userId, AddressType.Shipping);

            return View(vm);
        }

        [HttpGet]
        [Route("new-address")]
        public IActionResult NewAddress()
        {
            return View(new NewAddressViewModel());
        }

        [HttpPost]
        [Route("new-address")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAddress(NewAddressViewModel vm)
        {
            var userId = new Guid(_userManager.GetUserId(User));
            var domainAddress = Mapper.Map(vm).ToANew<Address>();

            await _userService.InsertUserAddressAsync(domainAddress, userId, AddressType.Shipping);
            return RedirectToAction("Index");
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
