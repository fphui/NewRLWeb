using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.ViewModels
{
    public class News_Notices
    {
        public List<News> news = new List<News>();
        public List<Notice> notices = new List<Notice>();
    }
}