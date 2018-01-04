using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.UserViewModel
{
    public class AddUserViewModel
    {
        [DisplayName("使用者代號")]
        public long User_no { get; set; }

        [Required(ErrorMessage = "姓名不能為空白")]
        [MaxLength(15, ErrorMessage = "姓名不能超過15個字  ")]
        [DisplayName("姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請選擇性別")]
        [DisplayName("性別")]
        public bool Sex { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "日期格式不正確")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("生日")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Email不能空白")]
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "電話號碼最多10個數字")]
        [Phone(ErrorMessage = "電話號碼格式錯誤")]
        [DisplayName("連絡電話1")]
        public string Phone_1 { get; set; }

        [MaxLength(10, ErrorMessage = "電話號碼最多10個數字")]
        [Phone(ErrorMessage = "電話號碼格式錯誤")]
        [DisplayName("連絡電話2")]
        public string Phone_2 { get; set; }
    }
}
