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
            Statistics
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
            { "新增電影", "Movie", "New" },
            { "電影查詢", "Movie", "Details" }
        };

        public static string[] GetTitleItem(Area area)
        {
            switch (area)
            {
                case Area.Admin:
                    return new string[] { "管理者", "Home", "Index" };

                case Area.User:
                    return new string[] { "會員", "User", "Details" };

                case Area.Movie:
                    return new string[] { "影片", "Movie", "Details" };

                case Area.Statistics:
                    return new string[] { "統計分析", "Statistics", "Index" };
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

                case Area.Statistics:
                    return movieItemArray;

            }

            return null;
        }
    }
}
