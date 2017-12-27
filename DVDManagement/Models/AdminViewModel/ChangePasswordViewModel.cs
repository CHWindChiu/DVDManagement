using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models.AdminViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "舊的密碼不能為空白")]
        [RegularExpression(@"[a-zA-Z0-9_]{4,20}", ErrorMessage = "密碼必須是英文字母或數字或_，長度4~20個字")]
        [DataType(DataType.Password)]
        [Display(Name = "舊的密碼")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "密碼不能為空白")]
        [RegularExpression(@"[a-zA-Z0-9_]{4,20}", ErrorMessage = "密碼必須是英文字母或數字或_，長度4~20個字")]
        [DataType(DataType.Password)]
        [Display(Name = "新的密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "請再輸入一次密碼")]
        [Compare("NewPassword", ErrorMessage = "再次輸入密碼與原本輸入的密碼不相同")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
