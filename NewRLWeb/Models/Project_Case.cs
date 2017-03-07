using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("项目案例")]
    public class Project_Case
    {
        #region 字段
        [Key]
        //[Required]
        [DisplayName("项目ID")]
        //[HiddenInput(DisplayValue = false)]
        public int ProjectID { get; set; }

        [DisplayName("项目名称")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [MaxLength(50)]
        [DataType(DataType.Text)] 
        public string Projectname { get; set; }

        //[DisplayName("团队成员")]
        //public string Team_members { get; set; }
        [DisplayName("起始日期")]
        [Required(ErrorMessage = "您需要选择{0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime Startdate { get; set; }

        
        [DisplayName("结束日期")]
        [Required(ErrorMessage = "您需要选择{0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime Enddate { get; set; }

        [DisplayName("图片")]
        public string PhotoAddress { get; set; }

        [DisplayName("正文")]
        //[Required(ErrorMessage = "您需要填写{0}")]
        public string Coverage { get; set; }

        
        [DisplayName("合作单位")]
        [MaxLength(100)]
        [DataType(DataType.MultilineText)]
        public string Cooperativeunit { get; set; }

        [DisplayName("类别")]
        [MaxLength(50)]
        public string ProjectType { get; set; }

        [DisplayName("简介")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [DataType(DataType.MultilineText)]
        public string Abstract { get; set; }
        #endregion
    }
        
}