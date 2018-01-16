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
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Newtonsoft.Json;
using DVDManagement.Models.UserViewModel;
using static DVDManagement.Models.Transaction;

namespace DVDManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly DVDMAGContext _context;

        public UserController(DVDMAGContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Modify(long user_no)
        {
            var userInfo = await _context.User.SingleOrDefaultAsync(m => m.User_no == user_no);

            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(User model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
             
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Info), new { user_no = model.User_no });
        }

        public IActionResult AddUser()
        {
            return View();
        }

        //新增客戶
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Entry(model).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        //客戶資訊
        [HttpGet]
        public async Task<IActionResult> Details(
            string sortOrder,
            string searchParam,
            string searchString,
            int? page)
        {
            if (searchParam != null)
            {
                ViewData["CurrentSearchParam"] = searchParam;
            }

            if (searchString != null)
            {
                page = 1;
            }

            IQueryable<User> users = from s in _context.User select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchParam)
                {
                    default:
                        users = users.Where(s => s.User_no.Equals(searchString));
                        break;
                    case "客戶代號":
                        users = users.Where(s => s.User_no.Equals(searchString));
                        break;
                    case "名稱":
                        users = users.Where(s => s.Name.Equals(searchString));
                        break;
                    case "Email":
                        users = users.Where(s => s.Email.Equals(searchString));
                        break;
                    case "電話":
                        users = users.Where(s => s.Phone_1.Equals(searchString)
                        || s.Phone_2.Equals(searchString));
                        break;
                }
            }

            //每頁資料筆數
            int pageSize = 5;
            return View(await PaginatedList<User>.CreateAsync(users, page ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Info(long user_no)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(m => m.User_no == user_no);

            if (user == null)
            {
                return NotFound();
            }

            //目前客戶所租的dvd
            var dvdList = _context.Dvd_info.Include(d => d.Dvd_recode)
                .Where(m => m.Dvd_recode.User_no == user_no && m.Dvd_recode.Status == 1).ToList();

            var userInfoViewModel = new UserInfoViewModel
            {
                Dvd_infoList = dvdList,
                User = user
            };

            return View(userInfoViewModel);
        }

        //交易紀錄
        [HttpGet]
        public async Task<IActionResult> TransactionDetails(long id, int? page)
        {
            //客戶的交易列表
            IQueryable<Transaction> transactions = from s in _context.Transaction select s;
            transactions = transactions
                .Where(m => m.User_no == id).OrderByDescending(m => m.Trans_date);

            if (transactions == null)
            {
                return NotFound();
            }

            ViewData["DetailId"] = id;

            //每頁資料筆數
            int pageSize = 5;
            return View(await PaginatedList<Transaction>.CreateAsync(transactions, page ?? 1, pageSize));
        }

        //儲值
        [HttpPost]
        public async Task<IActionResult> Deposit(long user_no, int amount)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(m => m.User_no == user_no);

            user.Store_amount += amount;

            await _context.SaveChangesAsync();

            //新增交易紀錄
            Transaction transaction = new Transaction
            {
                User_no = user_no,
                Type = (byte)TransactionType.Deposit,
                Amount = amount,
                Movie_code = 0,
                Trans_date = DateTime.Now
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(user.Store_amount);
        }

        //租片
        [HttpPost]
        public async Task<IActionResult> Rent(long user_no, long movie_code)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(m => m.User_no == user_no);

            if (user == null)
            {
                return NotFound();
            }

            var dvd = await _context.Dvd_info
                .Include(d => d.Dvd_recode).SingleOrDefaultAsync(m => m.Movie_code == movie_code);

            if (dvd == null)
            {
                return Ok("0");
            }

            //檢查dvd是否已出租
            if(dvd.Dvd_recode.Status == 1)
            {
                return Ok("1");
            }

            //金額不足
            if (dvd.Rent > user.Store_amount)
            {
                return Ok("2");
            }

            dvd.Dvd_recode.Status = 1;
            dvd.Dvd_recode.User_no = user_no;
            dvd.Dvd_recode.Rent_date = DateTime.Now;
            user.Store_amount -= dvd.Rent;

            await _context.SaveChangesAsync();

            //新增交易紀錄
            Transaction transaction = new Transaction
            {
                User_no = user_no,
                Type = (byte)TransactionType.Rent,
                Amount = dvd.Rent,
                Movie_code = movie_code,
                Movie_name = dvd.Name,
                Trans_date = DateTime.Now
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();

            string dvdInfoJsonObj = JsonConvert.SerializeObject(dvd);

            return Ok(dvdInfoJsonObj);
        }

        //還片
        [HttpPost]
        public async Task<IActionResult> ReturnDVD(long user_no, long movie_code)
        {
            var user = await _context.User
            .SingleOrDefaultAsync(m => m.User_no == user_no);

            if (user == null)
            {
                return NotFound();
            }

            var dvd = await _context.Dvd_info
            .Include(d => d.Dvd_recode).SingleOrDefaultAsync(m => m.Movie_code == movie_code
            && m.Dvd_recode.Status == 1 && m.Dvd_recode.User_no == user_no);

            if (dvd == null)
            {
                return Ok("0");
            }

            dvd.Dvd_recode.Status = 0;
            dvd.Dvd_recode.User_no = 0;

            //計算是否超過期限
            var dateTimeNow = DateTime.Now;

            TimeSpan ts1 = new TimeSpan(dateTimeNow.Ticks);
            TimeSpan ts2 = new TimeSpan(dvd.Dvd_recode.Rent_date.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            
            int overdueAmount = 0;

            //判斷是否超過租借期限,超過要付額外費用
            if(ts.Days >= dvd.Time_limit)
            {
                overdueAmount = dvd.Overdue;
            }

            user.Store_amount -= overdueAmount;

            await _context.SaveChangesAsync();

            //新增交易紀錄
            Transaction transaction = new Transaction
            {
                User_no = user_no,
                Type = (byte)TransactionType.ReturnDVD,
                Amount = overdueAmount,
                Movie_code = 0,
                Movie_name = null,
                Trans_date = DateTime.Now
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();

            string dvdInfoJsonObj = JsonConvert.SerializeObject(dvd);

            return Ok(dvdInfoJsonObj);
        }

        //刪除客戶
        [HttpPost]
        public async Task<IActionResult> Delete(long user_no)
        {
            //如果客戶還有DVD沒還，則不能刪除
            var dvd = _context.Dvd_recode.Where(m => m.User_no == user_no);
            
            if(dvd.Count() > 0)
            {
                return Ok("此客戶還有DVD沒歸還，不能刪除");
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.User_no == user_no);

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok("Ok");
        }
    }
}
