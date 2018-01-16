using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Models
{
    public class Dvd_info
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("影片代碼")]
        public long Movie_code { get; set; }

        [Required(ErrorMessage = "影片名稱不能為空白")]
        [MaxLength(15)]
        [DisplayName("影片名稱")]
        public string Name { get; set; }

        [Required]
        [Range(0, 4)]
        [DisplayName("影片類型")]
        // 0:劇情 1:動作 2:恐怖驚悚 3:喜劇 4:紀錄片
        public byte Type { get; set; }

        [Required(ErrorMessage = "出租金額不能為空")]
        [Range(0, 999, ErrorMessage = "出租金額不能超過999")]
        [DisplayName("出租金額")]
        public int Rent { get; set; }

        [Required(ErrorMessage = "逾期金額不能為空")]
        [Range(0, 999, ErrorMessage = "逾期金額不能超過999")]
        [DisplayName("逾期金額(每日)")]
        public int Overdue { get; set; }

        [Required(ErrorMessage = "到期日不能為空")]
        [DisplayName("到期日(天)")]
        [Range(1, 10, ErrorMessage = "到期日不能超過10天")]
        public byte Time_limit { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("建立日期")]
        public DateTime Create_date { get; set; }

        public Dvd_recode Dvd_recode { get; set; }
    }
}
