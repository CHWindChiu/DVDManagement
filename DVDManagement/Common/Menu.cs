using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Common
{
    public class Menu 
    {
        public enum Area
        {
            Admin,
            User,
            Movie,
        }

        private static string[,] adminItemArray = new string[,]
        {
            { "管理者註冊", "Admin", "Register" },
            { "管理者資料", "Admin", "Details" }
        };

        private static string[,] userItemArray = new string[,]
        {
            { "新增會員", "User", "AddUser" },
            { "會員資料", "User", "Details" }
        };

        private static string[,] movieItemArray = new string[,]
        {
            { "新增DVD", "DVD", "AddDVD" },
            { "DVD查詢", "DVD", "Details" }
        };

        public static string[] GetTitleItem(Area area)
        {
            switch (area)
            {
                case Area.Admin:
                    return new string[] { "管理者", "Admin"};

                case Area.User:
                    return new string[] { "會員", "User"};

                case Area.Movie:
                    return new string[] { "DVD", "DVD" };
            }

            return null;
        }

        public static string[,] GetMenuItem(Area area)
        {
            switch (area)
            {
                case Area.Admin:
                    return adminItemArray;

                case Area.User:
                    return userItemArray;

                case Area.Movie:
                    return movieItemArray;
            }

            return null;
        }
    }
}
