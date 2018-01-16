using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class Admin : IdentityUser
    {
        [Key]
        [Display(Name = "帳號")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Email不能空白")]
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        public override string Email { get; set; }

        [Display(Name = "電話號碼")]
        [MaxLength(10, ErrorMessage = "電話號碼最多10個數字")]
        [Phone(ErrorMessage = "電話號碼格式錯誤")]
        public override string PhoneNumber { get; set; }

        [Display(Name = "地址")]
        [MaxLength(100, ErrorMessage = "地址不能超過100字")]
        public string Address { get; set; }
    }
}
