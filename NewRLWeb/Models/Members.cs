using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("团队成员")]
    public class Members
    {
        [Key]
        public int ID { get; set; }
        
        [DisplayName("项目ID")]
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ProjectID{get;set;}

        [DisplayName("用户ID")]
        [Required]
        [MaxLength(10)]
        [HiddenInput(DisplayValue = false)]
        public string UserID { get; set; }

        [DisplayName("姓名")]
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string Username { get; set; }

        //[Required]
        //[DisplayName("大类别")]
        //[MaxLength(10)]
        //public string L_Type { get; set; }
    }
}