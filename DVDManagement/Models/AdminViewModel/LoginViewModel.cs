using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "帳號不能為空白")]
        //[RegularExpression(@"[a-zA-Z0-9_]{4,10}", ErrorMessage = "帳號必須是英文字母或數字，長度4~10個字")]
        [DisplayName("帳號")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼不能為空白")]
        //[RegularExpression(@"[a-zA-Z0-9_]{8,20}", ErrorMessage = "帳號必須是英文字母或數字，長度8~20個字")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "再次輸入密碼")]
        //[Compare("Password", ErrorMessage = "再次輸入密碼與原本輸入的密碼不相同")]
        //public string ConfirmPassword { get; set; }

        //[Required]
        //[Display(Name = "手機號碼")]
        //[MaxLength(10)]
        //public string PhoneNumber { get; set; }

        //[Required]
        //[MaxLength(100)]
        //[Display(Name = "地址")]
        //public string Address { get; set; }
    }
}
