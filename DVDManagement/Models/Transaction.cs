using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Models
{
    public class Transaction	
    {
        [Key]
        [Required]
        [StringLength(10)]
        [DisplayName("使用者代號")]
        public string User_no { get; set; }

        [Required]
        [Range(0,2)]
        [DisplayName("交易類型")]
        //0:儲值 1:租片付費
        public byte Type { get; set; }

        [Range(0, 999999)]
        [DisplayName("加值金額")]
        public int Add_amount { get; set; }

        [StringLength(10)]
        [DisplayName("影片代碼")]
        public string Movie_code { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("交易日期")]
        public DateTime Trans_date { get; set; }
    }
}
