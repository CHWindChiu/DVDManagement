using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "帳號不能為空白")]
        [RegularExpression(@"[a-zA-Z0-9_]{4,10}", ErrorMessage = "帳號必須是英文字母或數字或_，長度4~10個字")]
        [DisplayName("帳號")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼不能為空白")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}",
            ErrorMessage = "密碼需要至少一個大寫和小寫英文字母和數字，長度4~20個字")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "再次輸入密碼")]
        [Compare("Password", ErrorMessage = "再次輸入密碼與原本輸入的密碼不相同")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email不能空白")]
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        public string Email { get; set; }

        [Display(Name = "手機號碼")]
        [MaxLength(10, ErrorMessage = "電話號碼最多10個數字")]
        [Phone(ErrorMessage = "電話號碼格式錯誤")]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "照片")]
        public IFormFile AvatarImage { get; set; }
    }
}
