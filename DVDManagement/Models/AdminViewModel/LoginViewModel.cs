using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "帳號不能為空白")]
        [DisplayName("帳號")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼不能為空白")]
        public string Password { get; set; }
    }
}
