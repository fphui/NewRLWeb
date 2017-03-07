using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.ViewCode;
using NewRLWeb.Package;
namespace NewRLWeb.Controllers
{
    public class HomeController : Controller
    {
        private Logic_News db_news = new Logic_News();
        private Logic_Notice db_notice = new Logic_Notice();
        private Logic_Album db_album = new Logic_Album();
        private readonly int pageSize = 10;
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //Home_Index home_index =new Home_Index();
            //List<Users> user = home_index.search();
            //ViewData["user"] = user;
            return View();
        }

        public ActionResult Banner()
        {
            return View();
        }
        public ActionResult News()
        {
            List<News> news = db_news.SearchFirstN(3);
            return View(news);
        }
        /// <summary>
        /// 获取公告
        /// add by llt 2017.1.16
        /// </summary>
        /// <returns></returns>
        public ActionResult Notice()
        {
            int n = 4;//获取前四个公告
            List<Notice> notices = db_notice.SearchtheFirstN(n);
            return View(notices);
        }
        public ActionResult Photos()
        {
            int n = 8;//获取最新八个相册集
            List<Album> albums = db_album.SearchTopNum(n);
            return View(albums);
        }
        public ActionResult Aboutus()
        {
            return View();
        }
        public ActionResult SearchNews(String Type)
        {
            string searchWord = "";
            if (Request["SearchWord"] != null)
            {
                searchWord = Request["SearchWord"];
                Session["Word"] = searchWord;
            }
            else 
            {
                searchWord = Session["Word"].ToString();
            }
            List<News> news = db_news.SearchFuzzy(searchWord);
            int TotalCount = news.Count();
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.Word = searchWord;
            ViewBag.Type = "新闻";
            return View(news);
        }

        public ActionResult SearchNotice(String Type)
        {
            string searchWord = Session["Word"].ToString();
            List<Notice> notice = db_notice.SearchFuzzy(searchWord);
            int TotalCount = notice.Count();
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.Word = searchWord;
            ViewBag.Type = "通知";
            return View(notice);
        }
        public ActionResult AjaxPaging(int pageIndex,string Type)
        {
            string Word = Session["Word"].ToString();
            if (Type == "新闻")
            {
                List<News> n = db_news.SearchFuzzy(Word); //数据库取数据
                List<News> news = db_news.SearchPaging(n, pageIndex, pageSize);
                return PartialView("SearchNewsView", news.ToList());
            }
            else
            {
                List<Notice> n = db_notice.SearchFuzzy(Word); //数据库取数据
                List<Notice> notice = db_notice.SearchPaging(n, pageIndex, pageSize);
                return PartialView("SearchNoticeView", notice.ToList());
            }
        }
    }
}
