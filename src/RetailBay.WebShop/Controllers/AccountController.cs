using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Interfaces;
using RetailBay.WebShop.Models;

namespace RetailBay.WebShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Fields

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICartService _cartService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="cartService">The cart service.</param>
        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            ICartService cartService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _cartService = cartService;
        }

        #endregion Constructors

        #region Actions

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(vm);

            ViewData["ReturnUrl"] = returnUrl;

            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(vm.Username);
                await OnLoginSuccess(user.Id);
                return RedirectToAction("Index", "Home");
            }

            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
                    {
                        var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
                        await _cartService.AddUserToAnonymousCartAsync(user.Id, cartId);
                    }

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }                
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private async Task OnLoginSuccess(Guid userId)
        {
            // Transfer user cart if needed.
            if (Request.Cookies.ContainsKey(Constants.CART_COOKIE_NAME))
            {
                var cartId = new Guid(Request.Cookies[Constants.CART_COOKIE_NAME]);
                var userCartId = await _cartService.TransferAnonymousCartToUserAsync(userId, cartId);
                Response.Cookies.Append(Constants.CART_COOKIE_NAME, userCartId.ToString());
            }
        }

        #endregion Actions
    }
}
