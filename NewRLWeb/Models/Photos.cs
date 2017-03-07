using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("图片表")]
    public class Photos
    {
        #region 字段
        [Key]
        //[Required]
        public int PhotoID { get; set; }

        [Required]
        public int AlbumID { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [DisplayName("图片路径")]
        public string Address { get; set; }

        [DisplayName("描述")]
        [MaxLength(200)]
        public string Describe { get; set; }

        [Required]
        [MaxLength(10)]
        public string L_Type { get; set; }
        #endregion
    }
        
}