using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("大类别")]
    public class Large_type
    {
        #region 字段
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(10)]
        public string L_Type { get; set; }

        [DisplayName("是否可删")]
        [Range(0, 1)]
        public byte Retain { get; set; }
        #endregion
    }
        
}