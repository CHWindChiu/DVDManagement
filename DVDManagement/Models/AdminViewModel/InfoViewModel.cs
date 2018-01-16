using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class InfoViewModel
    {
        public Admin AdminModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
    }
}
