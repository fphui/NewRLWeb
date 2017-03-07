using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NewRLWeb.Models
{
    [DisplayName("毕业信息")]
    public class Employment_Information
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("唯一识别码")]
        [Required]
        public string Unique_ID { get; set; }

        public string UserID { get; set; }

        [Required]
        [DisplayName("毕业流向")]
        [MaxLength(50)]
        public string Graduate_information { get; set; }

        [DisplayName("添加日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Add_Data { get; set; }
    }
}