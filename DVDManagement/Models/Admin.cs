using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class Admin
    {
        [Key]
        [Required(ErrorMessage = "帳號不能為空白")]
        [MinLength(4)]
        [MaxLength(10)]
        [RegularExpression(@"[a-zA-Z0-9_]{4,10}", ErrorMessage = "帳號必須是英文字母或數字，長度4~10個字")]
        [DisplayName("帳號")]
        public string account { get; set; }

        [Required(ErrorMessage = "密碼不能為空白")]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression(@"[a-zA-Z0-9_]{8,20}", ErrorMessage = "帳號必須是英文字母或數字，長度8~20個字")]
        [DisplayName("密碼")]
        public string password { get; set; }

        [Required]
        [DisplayName("名稱")]
        [MaxLength(15)]
        public string name { get; set; }

        [Required]
        [MaxLength(254)]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MaxLength(10)]
        [Phone]
        public string phone { get; set; }

        //管理者權限代號
        [Required]
        [MaxLength(20)]
        public string permission { get; set; }
    }
}
