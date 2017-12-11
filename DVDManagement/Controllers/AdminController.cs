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

namespace DVDManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly DVDMAGContext _context;

        public AdminController(DVDMAGContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //取得登入的管理者資料
            Admin admin = JsonConvert.DeserializeObject<Admin>(TempData.Peek("adminAccount") as string);
            return View(admin);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("account,password")] Admin admin)
        {
            /*
            var result = await _context.Admin.SingleOrDefaultAsync(m => m.Account == adminUser.Account && m.Password == adminUser.Password);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            */

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
