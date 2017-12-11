using DVDManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(DVDMAGContext context)
        {
            string adminAccount = "Admin";
            string adminPassword = "1234";

            context.Database.EnsureCreated();

            Admin result = await context.Admin.SingleOrDefaultAsync(m => m.account == adminAccount && m.password == adminPassword);

            //如果Admin有資料就不建立初始資料
            if (result != null)
            {
                return;   
            }

            context.Admin.Add(new Admin { account="Admin", password="1234", name="admin", email="test2134@gmail.com", phone="0987654567", permission ="1" });
            context.SaveChanges();
        }
 
    }
}
