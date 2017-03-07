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
    [DisplayName("毕业详细信息")]
    public class Graduate
    {
        [DisplayName("个人信息")]
        [Required]
        public Users Person { get; set; }
        [DisplayName("毕业信息")]
        [Required]
        public List<Employment_Information> Informations { get; set; }
        [Required]
        public List<Photos> photos { get; set; }
        
    }
}