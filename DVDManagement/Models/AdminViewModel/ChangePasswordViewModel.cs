using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "舊的密碼不能為空白")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}",
            ErrorMessage = "密碼需要至少一個大寫和小寫英文字母和數字，長度4~20個字")]
        [DataType(DataType.Password)]
        [Display(Name = "舊的密碼")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "密碼不能為空白")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}",
            ErrorMessage = "密碼需要至少一個大寫和小寫英文字母和數字，長度4~20個字")]
        [DataType(DataType.Password)]
        [Display(Name = "新的密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "請再輸入一次密碼")]
        [Compare("NewPassword", ErrorMessage = "再次輸入密碼與原本輸入的密碼不相同")]
        public string ConfirmPassword { get; set; }
    }
}
