using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Models
{
    public class Dvd_info
    {
        [Key]
        [Required]
        [StringLength(10)]
        [DisplayName("影片代碼")]
        public string movie_code { get; set; }

        [Required]
        [MaxLength(15)]
        [DisplayName("影片名稱")]
        public string name { get; set; }

        [Required]
        [Range(0, 2)]
        [DisplayName("影片類型")]
        // 0:劇情 1:動作 2:恐怖驚悚 3:喜劇 4:紀錄片
        public byte type { get; set; }

        [Required]
        [Range(0,999)]
        [DisplayName("出租金額")]
        public int rent { get; set; }

        [Required]
        [Range(0, 999)]
        [DisplayName("逾期金額(每日)")]
        public int overdue { get; set; }

        [Required]
        [DisplayName("租借期限(天)")]
        [Range(0, 10)]
        public byte time_limit { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("建立日期")]
        public DateTime create_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("更新日期")]
        public DateTime update_date { get; set; }
    }
}
