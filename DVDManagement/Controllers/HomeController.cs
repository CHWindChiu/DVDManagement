using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DVDManagement.Models;
using System.Diagnostics;
using DVDManagement.Data;
using DVDManagement.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace DVDManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly DVDMAGContext _context;

        public HomeController(DVDMAGContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync([Bind("account,password")] Admin admin)
        {
            var result = await _context.Admin.SingleOrDefaultAsync(m => m.account == admin.account && m.password == admin.password);
            
            if (result != null)
            {
                string adminAccountStr = JsonConvert.SerializeObject(result);
                TempData["menu"] = Menu.Area.Home.ToString();
                TempData["adminAccount"] = adminAccountStr;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
