using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class User
    {
        [Key]
        [Required]
        [StringLength(10)]
        [DisplayName("使用者代號")]
        public string user_no { get; set; }

        [Required]
        [MaxLength(15)]
        [DisplayName("名稱")]
        public string name { get; set; }

        [Required]
        [DisplayName("性別")]
        public bool sex { get; set; }

        [Required]
        [Range(0,99999999)]
        [DisplayName("生日")]
        public byte birthday { get; set; }

        [Required]
        [MaxLength(254)]
        [EmailAddress]
        [DisplayName("Email")]
        public string email { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("連絡電話1")]
        public string phone_1 { get; set; }

        [MaxLength(10)]
        [DisplayName("連絡電話2")]
        public string phone_2 { get; set; }

        [Required]
        [Range(0, 999999)]
        [DisplayName("儲值金額")]
        public int store_amount { get; set; }
    }
}
