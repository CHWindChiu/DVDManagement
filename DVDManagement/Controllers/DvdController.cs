using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DVDManagement.Data;
using DVDManagement.Models.DvdViewModel;
using DVDManagement.Models;
using DVDManagement.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DVDManagement.Controllers
{
    public class DvdController : Controller
    {
        private readonly DVDMAGContext _context;

        public DvdController(DVDMAGContext context)
        {
            _context = context;
        }

        public IActionResult AddDVD()
        {
            return View();
        }

        //新增DVD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDVD(Dvd_info model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Create_date = DateTime.Now;

            _context.Entry(model).State = EntityState.Added;
            await _context.SaveChangesAsync();

            var lastDvdCode = _context.Dvd_info.LastOrDefault().Movie_code;
            _context.Dvd_recode.Add(
            new Dvd_recode {
                Movie_code = lastDvdCode,
                Status = 0,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        //DVD資訊
        [HttpGet]
        public async Task<IActionResult> Info(long movie_code)
        {
            var result = await _context.Dvd_info
           .Include(d => d.Dvd_recode).SingleOrDefaultAsync(m => m.Movie_code == movie_code);

            if (result == null)
            {
                return NotFound(); 
            }

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Modify(long movie_code)
        {
            var dvd = await _context.Dvd_info.SingleOrDefaultAsync(m => m.Movie_code == movie_code);

            if (dvd == null)
            {
                return NotFound();
            }

            return View(dvd);
        }

        //修改DVD資訊
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Dvd_info model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Dvd_info dvd = await _context.Dvd_info.Where(m => m.Movie_code == model.Movie_code).SingleOrDefaultAsync();
            dvd.Name = model.Name;
            dvd.Rent = model.Rent;
            dvd.Overdue = model.Overdue;
            dvd.Time_limit = model.Time_limit;
            dvd.Type = model.Type;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Info), new { movie_code = model.Movie_code });
        }

        //DVD清單
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
                ViewData["CurrentSearchParam"] = searchParam;
            }

            if (searchString != null)
            {
                page = 1;
            }

            IQueryable<Dvd_info> dvds = (from s in _context.Dvd_info select s).Include(d => d.Dvd_recode);

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchParam)
                {
                    default:
                        dvds = dvds.Where(s => s.Movie_code.Equals(searchString));
                        break;
                    case "影片代號":
                        dvds = dvds.Where(s => s.Movie_code.Equals(searchString));
                        break;
                    case "名稱":
                        dvds = dvds.Where(s => s.Name.Equals(searchString));
                        break;
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    dvds = dvds.OrderByDescending(s => s.Name);
                    break;
                case "date":
                    dvds = dvds.OrderBy(s => s.Create_date);
                    break;
                case "date_desc":
                    dvds = dvds.OrderByDescending(s => s.Create_date);
                    break;
                default:
                    dvds = dvds.OrderBy(s => s.Name);
                    break;
            }

            //每頁資料筆數
            int pageSize = 5;
            return View(await PaginatedList<Dvd_info>.CreateAsync(dvds, page ?? 1, pageSize));
        }

        //刪除DVD
        [HttpPost]
        public async Task<IActionResult> Delete(long movie_code)
        {
            var dvd = await _context.Dvd_info.Include(d => d.Dvd_recode)
                .SingleOrDefaultAsync(m => m.Movie_code == movie_code);

            //如果DVD租借中則不能刪除
            if(dvd.Dvd_recode.Status == 1)
            {
                return Ok("DVD租借中不能刪除");
            }

            if (dvd == null)
            {
                return NotFound();
            }

            _context.Dvd_info.Remove(dvd);
            await _context.SaveChangesAsync();

            return Ok("Ok");
        }
    }
}