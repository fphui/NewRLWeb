using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("通知公告")]
    public class Notice
    {
        #region 字段
        [Key]
        //[Required]
        //[HiddenInput(DisplayValue = false)]
        public int NoticeID { get; set; }

        [MaxLength(10)]
        [DisplayName("发布人")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [DisplayName("正文")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [DataType(DataType.MultilineText)]
        public string Coverage { get; set; }
        //[Required]
        [DisplayName("发布时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Publicationtime { get; set; }

        [DisplayName("类别")]
        public string Type { get; set; }
        #endregion
    }
        
}