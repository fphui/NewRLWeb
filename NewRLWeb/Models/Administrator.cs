using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("管理员")]
    public class Administrator
    {
        #region 字段
        [Key]
        [Required]
        [MaxLength(10)]
        [DisplayName("用户名")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
        [DisplayName("密码")]
        public string Password { get; set; }
        #endregion
    }
}