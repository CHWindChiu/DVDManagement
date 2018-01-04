using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("使用者代號")]
        public long User_no { get; set; }

        [Required]
        [MaxLength(15)]
        [DisplayName("名稱")]
        public string Name { get; set; }

        [Required]
        [DisplayName("性別")]
        public bool Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("生日")]
        public DateTime Birthday { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("連絡電話1")]
        public string Phone_1 { get; set; }

        [MaxLength(10)]
        [DisplayName("連絡電話2")]
        public string Phone_2 { get; set; }

        [Required]
        [Range(0, 999999)]
        [DisplayName("儲值金額")]
        public int Store_amount { get; set; }
    }
}
