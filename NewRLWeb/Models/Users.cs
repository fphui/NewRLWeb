using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("用户")]
    public class Users
    {
        #region 字段
        
        //[MaxLength(5)]
        [DisplayName("唯一识别码")]
        //[HiddenInput(DisplayValue = false)]
        public string Unique_ID { get; set; }
        [Key]
        //[RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "请输入正确的学号格式。")]
        [DisplayName("学号")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [StringLength(10, MinimumLength = 9, ErrorMessage = "长度必须在{2}和{1}位之间。")]
        //[DataType(DataType.Text)]
        [Remote("CheckUserID", "Users", ErrorMessage = "已存在该用户")]
        public string UserID { get; set; }

        [DisplayName("姓名")]
        [Required]
        [MaxLength(10)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        
        [DisplayName("密码")]
        [Required]
        //[StringLength(10, MinimumLength = 6, ErrorMessage = "{0}至少{2}位数。")]
        [MaxLength(50)]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("性别")]
        //[UIHint("Boolean")]
        [Range(0, 1)]
        public byte Sex { get; set; }

        [DisplayName("头像")]
        //[Required]
        [MaxLength(100)]
        public string Photo_Address { get; set; }

        [DisplayName("专业")]
        [MaxLength(10)]
        [DataType(DataType.Text)] 
        public string Subject { get; set; }

        [DisplayName("籍贯")]
        [MaxLength(20)]
        public string Native { get; set; }

        [DisplayName("家庭住址")]
        [MaxLength(50)]
        public string Address { get; set; }
        
        [StringLength(12, MinimumLength = 6)]
        [RegularExpression("[1-9][0-9]{4,}", ErrorMessage = "*请输入正确的QQ格式。")]
        public string QQ { get; set; }

        //[Display(Name = "电话号码", Description = "常用的联系电话（手机或固话），固话格式为：区号-号码。")]
        [DisplayName("电话号码")]
        [MaxLength(12)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^1[0-9]{10}$", ErrorMessage = "*请输入正确的电话号码。")]    
        public string Phone_Number { get; set; }

        [DisplayName("电子邮箱")]
        [MaxLength(20)]
        [Required(ErrorMessage = "您需要填写{0}")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[0-9a-z_]+@(([0-9a-z]+)[.]){1,2}[a-z]{2,3}$", ErrorMessage = " *请输入正确的邮箱格式 ")]
        public string E_mail { get; set; }

        [DisplayName("入学时间")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime Admission_Data { get; set; }

        [DisplayName("毕业时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}")]
        public DateTime ?Graduate_Data { get; set; }

        [DisplayName("毕业寄语")]
        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DisplayName("毕业去向")]
        [MaxLength(5)]
        public string Graduate_Direction { get; set; }

        [DisplayName("学历")]
        [DataType(DataType.Text)]
        //[UIHint("Boolean")]
        public byte Education { get; set; }

        [DisplayName("学习经历")]
        [MaxLength(50)]
        [DataType(DataType.MultilineText)]
        public string Learning_experience { get; set; }

        [DisplayName("相册ID")]
        public int AlbumID { get; set; }

        [DisplayName("工作地点")]
        [MaxLength(5)]
        public string Graduate_Address { get; set; }
        #endregion
    }    
}