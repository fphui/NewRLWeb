using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("学术交流")]
    public class Academic_Exchange
    {
        #region 字段
        [Key]
        //[DisplayName("ID")]
        public int AcademicID { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("学术名称")]
        public string AcademicName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("摘要")]
        public string Abstract { get; set; }

        [Required]
        [DisplayName("图片")]
        public string PhotoAddress { get; set; }

        [Required]
        [DisplayName("正文")]
        //[Required(ErrorMessage = "您需要填写{0}")]
        public string Coverage { get; set; }

        [DisplayName("开始日期")]
        [Required(ErrorMessage = "您需要选择{0}")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BeginTime { get; set; }


        [DisplayName("结束日期")]
        [Required(ErrorMessage = "您需要选择{0}")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
      
        [MaxLength(100)]
        [DisplayName("链接")]
        public string Link { get; set; }
        #endregion
    }
}