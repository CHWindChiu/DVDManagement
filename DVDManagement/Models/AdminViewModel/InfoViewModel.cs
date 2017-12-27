using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class InfoViewModel
    {
        [Display(Name = "帳號")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email不能空白")]
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        public string Email { get; set; }

        [Display(Name = "手機號碼")]
        [MaxLength(10)]
        [Phone(ErrorMessage = "電話號碼格式錯誤")]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        public Admin adminModel { get; set; }

        public ChangePasswordViewModel changePasswordViewModel { get; set; }
    }
}
