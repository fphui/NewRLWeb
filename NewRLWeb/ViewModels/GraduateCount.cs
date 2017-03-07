using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using NewRLWeb.Models;

namespace NewRLWeb.ViewModels
{
    public class GraduateCount
    {
        [DisplayName("毕业年份")]
        [DataType(DataType.Date)]
        [Required]
        public int GraduYears { get; set; }
        [DisplayName("当年本科生工作人数")]
        [Required]
        public int GraduUWorknum { get; set; }
        [DisplayName("当年研究生工作人数")]
        [Required]
        public int GraduYWorknum { get; set; }
        [DisplayName("当年考研人数")]
        [Required]
        public int GraduYnum { get; set; }
        [DisplayName("当年考博人数")]
        [Required]
        public int GraduBnum { get; set; }
        [DisplayName("当年本科生毕业人数")]
        [Required]
        public int UGraduates { get; set; }
        [DisplayName("当年研究生毕业人数")]
        [Required]
        public int YGraduates { get; set; }
    }
}