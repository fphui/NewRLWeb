using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.ViewModels
{
    public class UserLogin
    {
        //[DisplayName("会员学号")]
        //[DataType(DataType.Text)]
        //[RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "请输入正确的学号格式。")]
        //public string userID { get; set; }

        //[DataType(DataType.Text)]
        //[DisplayName("会员姓名")]

        //public string username { get; set; }
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@" ^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$ ", ErrorMessage = " *请输入正确的邮箱格式 ")]
        //public string e_mail { get; set; }
        [DisplayName("会员账号")]
        [DataType(DataType.Text)]
        public string account { get; set; }

        [DisplayName("会员密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入{0}")]
        public string password { get; set; }

        [Display(Name = "验证码", Description = "请输入图片中的验证码。")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "×")]
        public string VerificationCode { get; set; }
    }
}