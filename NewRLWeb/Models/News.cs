using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("新闻")]
    public class News
    {
        #region 字段
        [Key]
        //[Required]
        [HiddenInput(DisplayValue = false)]
        public int NewsID { get; set; }

        [DisplayName("标题")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [MaxLength(50)]
        public string Title { get; set; }

        //[DisplayName("小标题")]
        //[Required(ErrorMessage = "您需要填写{0}")]
        //[MaxLength(30)]
        //public string Smalltitle { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("作者")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [MaxLength(10)]
        public string Author { get; set; }

        [DisplayName("摘要")]
        [Required(ErrorMessage = "您需要填写{0}")]
        [MaxLength(100)]
        public string Abstract { get; set; }

        [DisplayName("图片")]
        public string PhotoAddress { get; set; }

        [DisplayName("内容路径")]
        public string Coverage { get; set; }

        [DisplayName("txt路径")]
        public string CoverageTxt { get; set; }


        [DisplayName("类别")]
        [Required]
        [MaxLength(10)]
        public string News_Type { get; set; }

        [DisplayName("发布日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Publicationtime { get; set; }

        [DisplayName("浏览次数")]
        public int Glance_Number { get; set; }

        [DisplayName("新闻链接")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Link { get; set; }

        [DisplayName("新闻来源")]
        [DataType(DataType.Text)]
        [MaxLength(300)]
        public string NewsSource { get; set; }
        #endregion
    }
}