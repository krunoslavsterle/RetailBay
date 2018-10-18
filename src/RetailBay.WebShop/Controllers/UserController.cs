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
    public class UserController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="userService">The user service.</param>
        public UserController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        #endregion Constructors

        #region Properties
        #endregion Properties

        #region Methods

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = Mapper.Map(user).ToANew<ProfileViewModel>();

            return View(vm);
        }

        [HttpGet]
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

        #endregion Methods
    }
}
