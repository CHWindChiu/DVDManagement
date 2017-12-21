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
using Microsoft.AspNetCore.Identity;

namespace DVDManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly DVDMAGContext _context;
        private readonly UserManager<Admin> _userManager;
        private readonly SignInManager<Admin> _signInManager;

        public HomeController(
            DVDMAGContext context,
            UserManager<Admin> userManager,
            SignInManager<Admin> signInManager
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var userInfo = await _userManager.GetUserAsync(HttpContext.User);
            return View(userInfo);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
