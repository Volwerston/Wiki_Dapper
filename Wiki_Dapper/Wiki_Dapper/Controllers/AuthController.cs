using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wiki_Dapper.Entities.Models;
using Wiki_Dapper.Models.View;

namespace Wiki_Dapper.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        public IActionResult Register()
        {
            RegisterViewModel toAdd = new RegisterViewModel();

            return View(toAdd);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel toRegister)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = toRegister.Login
            };

            try
            {
                IdentityResult result
            = await userManager.CreateAsync(user, toRegister.Password);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }        
        }

        public IActionResult Login(string returnUrl)
        {
            LoginViewModel toAdd = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;

            return View(toAdd);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel toLogin, string returnUrl)
        {
            ApplicationUser appUser = await userManager.FindByNameAsync(toLogin.Login);

            if(appUser != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, toLogin.Password, false, false);
            }

            return Redirect(returnUrl ?? "/");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}