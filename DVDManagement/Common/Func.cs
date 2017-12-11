using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MiramarAdmin.Common
{
    public class Func
    {
        //取得class的屬性的DisplayName
        public static string GetDisplayName(Type classType, string name)
        {
            MemberInfo property = classType.GetProperty(name);
            var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single();
            return attribute.DisplayName;
        }

        //Excel
        public static string SetExcelName(string name)
        {
            DateTime now = DateTime.Now;
            return name + "_" + $"{now.Year}{now.Month}{now.Day}{now.Hour}{now.Minute}{now.Second}.xlsx";
        }

        public static Cell ConstructCell(string value, CellValues dataType, uint styleIndex = 0)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex
            };
        }

        public static Column ConstructColumn(UInt32 min, UInt32 max, int width, bool isCustomWidth)
        {
            return new Column()
            {
                Min = min,
                Max = max,
                Width = width,
                CustomWidth = isCustomWidth
            };
        }
    }
}
