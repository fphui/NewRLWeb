using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;
//using PagedList;
namespace NewRLWeb.ViewCode
{
    /// <summary>
    /// 新闻动态主页
    /// </summary>
    public class N_News
    {
        private vmContext vm = new vmContext();
        private rlwzContext rl = new rlwzContext();
        public News_Notices search(int id = 1, int pageIndex = 1,int pageSize = 5)
        {
            try
            {
                News_Notices newsnotices = new News_Notices();
                //newsnotices.news = rl.news.OrderByDescending(a => a.Publicationtime).ToPagedList(pageIndex, pageSize).ToList();
                //newsnotices.notices = rl.notice.OrderByDescending(a => a.Publicationtime).ToPagedList(id, pageSize).ToList();
                return newsnotices;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}