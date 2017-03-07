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
    public class Email
    {
        [DisplayName("主题")]
        public string mailSubject { get; set; }
        [DisplayName("内容")]
        public string mailContent { get; set; }
        [DisplayName("接收方")]
        public List<EmailTo> emailTo { get; set; }
    }
}