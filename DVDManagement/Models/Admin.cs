using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class Admin : IdentityUser
    {
        [DisplayName("帳號")]
        [MaxLength(50)]
        public override string UserName { get; set; }

        [DisplayName("電話號碼")]
        [MaxLength(10)]
        public override string PhoneNumber { get; set; }

        [DisplayName("地址")]
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
