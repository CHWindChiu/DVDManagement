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
        [StringLength(10)]
        [DisplayName("影片代碼")]
        public string Movie_code { get; set; }

        [Required]
        [Range(0, 1)]
        [DisplayName("狀態")]
        // 0:租借中 1:未租借
        public byte Type { get; set; }

        [StringLength(10)]
        [DisplayName("租借人代號")]
        public string User_no { get; set; }
    }
}
