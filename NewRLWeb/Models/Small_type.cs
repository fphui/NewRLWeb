using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("小类别")]
 public class Small_Type
 {
        #region 字段
        [Key]
        public int ID { get; set; }

        [MaxLength(10)]
        [Required]
        public string S_Type { get; set;}

        [MaxLength(10)]
        [Required]
        public string L_Type { get; set; }

        [DisplayName("是否可删")]
        [Range(0, 1)]
        [Required]
        public byte Retain { get; set; }
        #endregion
 }
     
}