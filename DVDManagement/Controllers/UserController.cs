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
using DVDManagement.Models.AdminViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using DVDManagement.Common;
using System.Linq;

namespace DVDManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly DVDMAGContext _context;

        private readonly UserManager<Admin> _userManager;
        private readonly SignInManager<Admin> _signInManager;
        private readonly ILogger _logger;
        private IHostingEnvironment _hostingEnvironment;

        public UserController(DVDMAGContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userInfo = await _userManager.GetUserAsync(HttpContext.User);
            return RedirectToAction("Info", userInfo);
        }

        public async Task<IActionResult> Info(string userName)
        {
            var userInfo = await _userManager.FindByNameAsync(userName);
            InfoViewModel model = new InfoViewModel
            {
                adminModel = userInfo
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(InfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok("驗證失敗");
            }

            var user = await _userManager.FindByNameAsync(model.adminModel.UserName);
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("更新成功");
            }
            else
            {
                return Ok("更新失敗");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // _logger.LogInformation(1, "User logged in.");
                    return RedirectToAction("Index", "admin");
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
                    return PartialView(model);
                }
            }

            return PartialView(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new Admin { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber, Address = model.Address };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.AvatarImage != null)
                {
                    string imgName = model.UserName;

                    if (model.AvatarImage.FileName.Contains("jpg"))
                    {
                        imgName += ".jpg";
                    }
                    else
                    {
                        imgName += ".png";
                    }

                    var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "AvatarImage", imgName);
                    if (model.AvatarImage != null)
                    {
                        using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await model.AvatarImage.CopyToAsync(fileStream);
                        }
                    }
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                //await _signInManager.SignInAsync(user, isPersistent: false);
                //_logger.LogInformation(3, "User created a new account with password.");
                return RedirectToAction("Index");
            }

            AddErrors(result);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(
            string sortOrder,
            string currentFilter,
            string searchParam,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            if (searchParam != null)
            {
                ViewData["CurrentFilterType"] = searchParam;
            }
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var allAdmins = _userManager.Users;
            IQueryable<Admin> admins;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchParam)
                {
                    default:
                        admins = allAdmins.Where(s => s.UserName.Equals(searchString));
                        break;
                    case "帳號":
                        admins = allAdmins.Where(s => s.UserName.Equals(searchString));
                        break;
                    case "Email":
                        admins = allAdmins.Where(s => s.Email.Equals(searchString));
                        break;
                    case "電話":
                        admins = allAdmins.Where(s => s.PhoneNumber.Equals(searchString));
                        break;
                }
            }
            else
            {
                admins = allAdmins;
            }
            //var admins = _context.Admin.ToListAsync();
            //var admins = from s in _context.Admin select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    admins = admins.Where(s => s.LastName.Contains(searchString)
            //                           || s.FirstMidName.Contains(searchString));
            //}
            //_context.Admin.ForEachAsync()
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        admins = admins.OrderByDescending(s => s.LastName);
            //        break;
            //    case "Date":
            //        admins = admins.OrderBy(s => s.EnrollmentDate);
            //        break;
            //    case "date_desc":
            //        admins = admins.OrderByDescending(s => s.EnrollmentDate);
            //        break;
            //    default:
            //        admins = admins.OrderBy(s => s.LastName);
            //        break;
            //}

            //每頁資料筆數
            int pageSize = 5;
            return View(await PaginatedList<Admin>.CreateAsync(admins.AsNoTracking(), page ?? 1, pageSize));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(InfoViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Ok("驗證失敗");
            //}

            var user = await _userManager.FindByNameAsync(model.adminModel.UserName);
            var changePasswordResult = await _userManager.ChangePasswordAsync(
                user, model.changePasswordViewModel.OldPassword,
                model.changePasswordViewModel.NewPassword
                );

            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return Ok("密碼修改失敗");
            }

            return Ok("密碼修改成功");
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
