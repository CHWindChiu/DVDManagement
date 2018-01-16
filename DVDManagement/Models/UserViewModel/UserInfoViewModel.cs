using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models.UserViewModel
{
    public class UserInfoViewModel
    {
        public List<Dvd_info> Dvd_infoList { get; set; }
        public List<Transaction> TransactionList { get; set; }
        public User User { get; set; }
    }
}
