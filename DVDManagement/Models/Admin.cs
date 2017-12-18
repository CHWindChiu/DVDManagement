using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class Admin : IdentityUser
    {
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
