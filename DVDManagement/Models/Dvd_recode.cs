using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Models
{
    public class Dvd_recode
    {
        [Key]
        [Required]
        [DisplayName("影片代碼")]
        public long Movie_code { get; set; }

        [Required]
        [Range(0, 1)]
        [DisplayName("狀態")]
        // 0:租借中 1:未租借
        public byte Status { get; set; }

        [StringLength(10)]
        [DisplayName("租借人代號")]
        public long User_no { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("租片日期")]
        public DateTime Rent_date { get; set; }
    }
}
