using System;
using Microsoft.AspNetCore.Mvc;
using DVDManagement.Models;
using DVDManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using DVDManagement.Common;
using System.Linq;
using DVDManagement.Models.UserViewModel;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

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

        public async Task<IActionResult> Info(long User_no)
        {
            var userInfo = await _context.User.SingleOrDefaultAsync(m => m.User_no == User_no);

            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        public async Task<IActionResult> Modify(long User_no)
        {
            var userInfo = await _context.User.SingleOrDefaultAsync(m => m.User_no == User_no);

            if (userInfo == null)
            {
                return NotFound();
            }

            InfoViewModel model = new InfoViewModel
            {
                userModel = userInfo
            };

            return View(model);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Modify(InfoViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var user = await _userManager.FindByNameAsync(model.adminModel.UserName);
        //    user.Email = model.Email;
        //    user.PhoneNumber = model.PhoneNumber;
        //    user.Address = model.Address;

        //    var result = await _userManager.UpdateAsync(user);
        //    if (result.Succeeded)
        //    {
        //        RedirectToAction(nameof(Details), userInfo);
        //    }
        //    else
        //    {
        //        return Ok("更新失敗");
        //    }
        //}

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = new User {
                Name = model.Name,
                Sex = model.Sex,
                Birthday = model.Birthday,
                Email = model.Email,
                Phone_1 = model.Phone_1,
                Phone_2 = model.Phone_2,
            };

            //_context.Entry(model).State = EntityState.Added;

            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
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
            
            IQueryable<User> admins = from s in _context.User select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchParam)
                {
                    default:
                        admins = admins.Where(s => s.Email.Equals(searchString));
                        break;
                    case "Email":
                        admins = admins.Where(s => s.Email.Equals(searchString));
                        break;
                    case "電話":
                        admins = admins.Where(s => s.Phone_1.Equals(searchString));
                        break;
                }
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
            return View(await PaginatedList<User>.CreateAsync(admins, page ?? 1, pageSize));
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
