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
    public class EmailTo
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("邮箱")]
        public string Mail { get; set; }
    }
}