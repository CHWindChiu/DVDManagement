using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class Admin : IdentityUser
    {
        [Key]
        public override string UserName { get; set; }

        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Address { get; set; }
    }
}
