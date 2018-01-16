using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Models
{
    public class Transaction	
    {
        public enum TransactionType
        {
            Deposit = 0,
            Rent = 1,
            ReturnDVD = 2
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("使用者代號")]
        public long User_no { get; set; }

        [Required]
        [Range(0,2)]
        [DisplayName("交易類型")]
        //0:儲值 1:租片 2:還片
        public byte Type { get; set; }

        [Range(0, 999999)]
        [DisplayName("金額")]
        public int Amount { get; set; }

        [DisplayName("影片代碼")]
        public long? Movie_code { get; set; }

        [DisplayName("影片名稱")]
        public string Movie_name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("交易日期")]
        public DateTime Trans_date { get; set; }
    }
}
