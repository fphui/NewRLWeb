using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;

namespace NewRLWeb.ViewCode
{
    /// <summary>
    /// 网站主页
    /// </summary>
    public class Home
    {
        private Logic_Users user = new Logic_Users();
        private Logic_News news= new Logic_News();
        private Logic_Notice notice = new Logic_Notice();
        private Logic_Photos photos = new Logic_Photos();
        private Logic_Album album = new Logic_Album();
        public List<Users> search()
        {
            return user.Search();
        }
    }
}  