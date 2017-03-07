using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("管理条例")]
    public class Rules_Management
    {
        #region 字段
        [Key]
        public int ID { get; set; }

        
        [DisplayName("发布人")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [MaxLength(10)]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [DisplayName("内容")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "您需要填写{0}")]
        public string Coverage { get; set; }

        [DisplayName("发布时间")]
        //[Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Publicationtime { get; set; }
        #endregion
    }
        
}