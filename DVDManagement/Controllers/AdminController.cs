using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DVDManagement.Models;
using System.Diagnostics;
using DVDManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DVDManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly DVDMAGContext _context;

        private readonly UserManager<Admin> _userManager;
        private readonly SignInManager<Admin> _signInManager;
        private readonly ILogger _logger;

        public AdminController(DVDMAGContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email,
                    model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                   // _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Admin model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = new Admin { account = model.account, name = model.name, email = model.Email, phone = model.phone  };
            //    var result = await _userManager.CreateAsync(user, model.password);
            //    if (result.Succeeded)
            //    {
            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            //        // Send an email with this link
            //        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //        //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            //        //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        //_logger.LogInformation(3, "User created a new account with password.");
            //        return RedirectToAction(nameof(HomeController.Index), "Home");
            //    }
            //    AddErrors(result);
            //}

            //// If we got this far, something failed, redisplay form
            //return View(model);
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }
}
