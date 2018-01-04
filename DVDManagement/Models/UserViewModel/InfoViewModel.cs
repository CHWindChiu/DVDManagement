using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.UserViewModel
{
    public class InfoViewModel
    {
        public AddUserViewModel addUserViewModel { get; set; }
        public User userModel { get; set; }
    }
}
