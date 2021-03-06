﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Entities.Identity;
using RetailBay.WebAdministration.Models;

namespace RetailBay.WebAdministration.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //await Infrastructure.EntityFramework.IdentityDBContext.SeedAsync(_userManager, _roleManager);

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
                return RedirectToAction("Index", "Home");

            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }
    }
}
