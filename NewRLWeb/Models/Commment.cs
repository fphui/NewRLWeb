using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("评论")]
    public class Commment
    {
        #region 字段
        [Key]
        public int ID { get; set; }

        [Required]
        public int Type_ID { get; set; }

        [Required]
        [DisplayName("用户学号")]
        [MaxLength(9)]
        public string UserID { get; set; }

        [Required]
        [DisplayName("用户")]
        [MaxLength(10)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("评论内容")]
        [MaxLength(300)]
        public string CommentText { get; set; }

        //[Required]
        [DisplayName("评论时间")]
        [DataType(DataType.DateTime)]
        public DateTime CommentTime { get; set; }

        [Required]
        [DisplayName("大类")]
        [MaxLength(10)]
        public string Large_Type { get; set; }
        #endregion
    }
       
}