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
            Home,
            Admin,
            User,
            Movie,
            Statistics
        }

        private static string[,] homeItemArray = new string[,]
        {
            { "管理者", "Admin", "Index" },
            { "使用者", "User", "Index" },
            { "影片", "Movie", "Index" },
            { "統計分析", "Statistics", "Index" }
        };

        private static string[,] adminItemArray = new string[,]
        {
            { "管理者註冊", "Admin", "Register" },
            { "管理者資料", "Admin", "Details" }
        };

        private static string[,] userItemArray = new string[,]
        {
            { "新增客戶", "User", "New" },
            { "客戶資料", "User", "Details" }
        };

        private static string[,] movieItemArray = new string[,]
        {
            { "新增電影", "Movie", "New" },
            { "電影查詢", "Movie", "Details" }
        };

        public static string[,] GetMenuItem(Area area)
        {
            switch (area)
            {
                case Area.Home:
                    return homeItemArray;

                case Area.Admin:
                    return adminItemArray;

                case Area.User:
                    return userItemArray;

                case Area.Movie:
                    return movieItemArray;

                default:
                    return homeItemArray;
            }
        }
    }
}
