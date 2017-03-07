using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("相册集")]
    public class Album
    {
        #region 字段
        [Key]
        //[Required]
        [HiddenInput(DisplayValue = false)]
        public int AlbumID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("相册名")]
        [MaxLength(20)]
        public string Albumname { get; set; }

        [Required]
        [DisplayName("展示图片路径")]
        [MaxLength(100)]
        public string Pho_Address { get; set; }


        [Required]
        //[DataType(DataType.Url)]
        [DisplayName("文件夹路径")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Folderpath { get; set; }

        
        [DisplayName("链接路径")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Link { get; set; }

        [DisplayName("相册类别")]
        [MaxLength(10)]
        public string Album_Type { get; set; }

        [DisplayName("发布日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Publicationtime { get; set; }
        #endregion
    }
}