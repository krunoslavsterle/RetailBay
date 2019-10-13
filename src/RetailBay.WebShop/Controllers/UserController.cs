using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces;
using RetailBay.WebShop.Models.User;

namespace RetailBay.WebShop.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="orderService">The order service.</param>
        public UserController(UserManager<ApplicationUser> userManager, IUserService userService, IOrderService orderService)
        {
            _userManager = userManager;
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = Mapper.Map(user).ToANew<ProfileViewModel>();

            return View(vm);
        }

        [HttpGet]
        [Route("addresses")]
        public async Task<IActionResult> Addresses()
        {
            var userId = _userManager.GetUserId(User);
            var addresses = await _userService.GetUserAddressesAsync(new Guid(userId));
            var shippingAddresses = addresses.Where(p => p.AddressType == AddressType.Shipping).Select(p => p.Address);
            var billingAddresses = addresses.Where(p => p.AddressType == AddressType.Billing).Select(p => p.Address);
            
            var vm = new AddressesViewModel
            {
                ShippingAddresses = Mapper.Map(shippingAddresses).ToANew<IEnumerable<AddressesViewModel.AddressDTO>>(),
                BillingAddresses = Mapper.Map(billingAddresses).ToANew<IEnumerable<AddressesViewModel.AddressDTO>>()
            };

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
            return RedirectToAction("Addresses");
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> Orders()
        {
            var userId = new Guid(_userManager.GetUserId(User));

            var orders = await _orderService.GetOrdersForUserAsync(userId);
            return View(orders);
        }
    }
}
