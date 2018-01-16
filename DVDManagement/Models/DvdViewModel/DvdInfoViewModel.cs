using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models.DvdViewModel
{
    public class DvdInfoViewModel
    {
        public Dvd_info Dvd_info { get; set; }
        public Dvd_recode Dvd_recode { get; set; }
    }
}
