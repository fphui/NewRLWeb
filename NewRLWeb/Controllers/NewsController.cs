using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.Package;
//using PagedList;
using NewRLWeb.ViewCode;
using NewRLWeb.ViewModels;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Globalization;
using NewRLWeb.Filters;
//using System.Globalization.DateTimeFormatInfo;

namespace NewRLWeb.Controllers
{
    public class NewsController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private N_News nn = new N_News();
        private Logic_News db_news = new Logic_News();
        private Logic_Comment db_com = new Logic_Comment();
        private Logic_Small_Type db_stype = new Logic_Small_Type();
        private static List<SelectListItem> listItem_type; //下拉框
        private readonly int pageSize = 10;  //每页显示数量
        private static int pageCount; //数据数量
        private static int? choose;
        private static int fileid;//上传文件id

        #region 新闻页面
        //
        // GET: /News/

        public ActionResult Index(string type)
        {
            if (type == null || type == "全部")
            {
                List<News> news = db_news.Search();
                pageCount = news.Count();
                ViewBag.PageSize = pageSize;
                ViewBag.TotalCount = pageCount;
                ViewBag.Type = "全部";
                return View(news);
            }
            else
            {
                List<News> news = db_news.SearchByType(type);
                pageCount = news.Count();
                ViewBag.PageSize = pageSize;
                ViewBag.TotalCount = pageCount;
                ViewBag.Type = type;
                return View(news);
            }

        }

        public List<Notice> GetPerson_notice(int pageIndex, int pageSize, ref int totalCount, List<Notice> graduate)
        {
            var persons = graduate.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = graduate.Count();
            return persons.ToList();
        }

        public ActionResult News_View(int id)
        {
            News news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        public ActionResult Comment(int Newsid)
        {
            List<Commment> com = db_com.GetComment(Newsid, "新闻");
            return View(com);
        }

        public ActionResult GetComment(int id)
        {
            Commment com = new Commment();
            News news = db.news.Find(id);
            com.Large_Type = "新闻";
            com.Type_ID = id;
            com.UserID = Session["userId"].ToString();
            com.Username = Session["username"].ToString();
            com.CommentTime = System.DateTime.Now;
            com.CommentText = Request["content"];
            if (com.CommentText != null)
                db_com.AddModel(com);
            return PartialView("News_View", news);
            //News news = db.news.Find(id);
            //return View("News_View", news);
        }
        #endregion

        #region 管理新闻 增 删 改 细节
        // 新闻添加页面
        // GET: /News/Create
        [CheckLogin]
        public ActionResult Create()
        {
            //List<Small_Type> news_type = db_stype.Search("新闻"); //取出新闻类别
            //listItem_type = new List<SelectListItem>();  //年份下拉框的绑定
            //foreach (Small_Type n_type in news_type)
            //{
            //    listItem_type.Add(new SelectListItem { Text = n_type.S_Type, Value = n_type.S_Type });
            //}
            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = false;
            return PartialView();
        }
        //
        // POST: /News/Create
        // 新闻新增+添加html文件       
        ///周煜 2016 12 08
        ///change by llt 2017.2.1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                var fileHtm = Request.Files["UpLoadHtm"];
                var fileTxt = Request.Files["UpLoadTxt"];
                var fileEnc = Request.Files["UpLoadEnc"];
                //if (files == null)
                //{
                //    Response.Write("<script>alert('文件上传失败，请重新上传！')</script>");
                //    ViewBag.str = "0";
                //}
                //else
                //{
                if (db_news.HasSameNews(news.Title))
                {
                    ModelState.AddModelError("Title", "已有同名新闻");
                    //Response.Write("<script>alert('添加失败！ ')</script>");
                }
                else
                {
                    news.Publicationtime = DateTime.Now; //当前时间
                    if (db_news.Add(news))
                    {
                        news = db_news.SearchbyTitle(news.Title);
                        if (UploadHtm(news.NewsID, fileHtm) && UploadTxt(news.NewsID, fileTxt) && UploadEnclosure(news.NewsID, fileEnc))
                        {
                            fileid = news.NewsID;
                            var img = Request.Files["xdaTanFileImg"];
                            if (img != null && img.ContentLength > 0)
                            {
                                HttpPostedFileBase image = img;
                                //判定文件的大小
                                double dFileSize = image.ContentLength;
                                if (dFileSize > 5242880)//1024*1024*5)
                                {
                                    Response.Write("<script>alert('" + image.FileName + "文件大于5MB')</script>");
                                }
                                else if (dFileSize != 0)
                                {
                                    string timenow = DateTime.Now.ToString("yyyyMMddhhmmss");//得到系统时间
                                    string path = "~/Html/News/" + news.NewsID + "/";
                                    //Directory.CreateDirectory(Server.MapPath(path));
                                    string fFullDir = path + "title" + timenow + image.FileName;
                                    image.SaveAs(Server.MapPath(fFullDir));
                                    news.PhotoAddress = fFullDir;
                                    db_news.Update(news);
                                }
                            }
                            return RedirectToAction("List");
                        }
                        else
                            Response.Write("<script>alert('文件上传失败，请重新上传！')</script>");
                    }
                    else
                        Response.Write("<script>alert('新闻添加失败！')</script>");
                }
            }
            else
                Response.Write("<script>alert('新闻添加失败！')</script>");
            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            return PartialView(news);
        }

        //
        // GET: /News/Delete/5
        // 新闻删除细节
        public ActionResult Delete(int id = 0)
        {
            //News news = db.news.Find(id);
            News news = db_news.SearchbyID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return PartialView(news);
        }
        //
        // POST: /News/Delete/5
        // 新闻单条删除
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (db_news.Delete(id))
                return RedirectToAction("List");
            else
            {
                Response.Write("<script>alert('删除失败！')</script>");
                News news = db_news.SearchbyID(id);
                return PartialView("Delete", news);
            }
        }
        /// <summary>
        /// 新闻多条删除
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultDelete(FormCollection collection)
        {
            bool DeleteStation = false;
            string[] ids = { };
            if (collection.GetValues("checks") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL;
            {
                string strids = collection.GetValue("checks").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                ids = strids.Split(',');
                foreach (string getid in ids)
                {
                    int ID = Convert.ToInt32(getid);
                    DeleteStation = db_news.Delete(ID);
                }
                //Response.Write("<script></script>");//成功删除
            }
            Session["DeleteStation"] = DeleteStation;
            if (DeleteStation)
                Response.Write("<script>alert('删除成功！')</script>");
            else
                Response.Write("<script>alert('删除失败！')</script>");
            return RedirectToAction("DeleteList");
        }

        // 新闻编辑页面
        // GET: /News/Edit/5
        public ActionResult Edit(int id = 0)
        {
            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            News news = db_news.SearchbyID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Reload = false;
            return PartialView(news);
        }
        //
        // POST: /News/Edit/5
        // 新闻编辑+修改图片、html文件
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (db_news.HasSameNews(news))
                {
                    ModelState.AddModelError("Title", "已有同名新闻");
                    //Response.Write("<script>alert('添加失败！ ')</script>");
                }
                else if (news.Publicationtime.CompareTo(DateTime.Now) > 0)
                {
                    ModelState.AddModelError("Publicationtime", "发布日期超出当前时间");
                }
                else
                {
                    var files = Request.Files["xdaTanFileImg"];
                    if (files != null && files.ContentLength > 0) //替换显示图片
                    {
                        HttpPostedFileBase fileitem = files;
                        //判定文件的大小
                        double dFileSize = fileitem.ContentLength;
                        if (dFileSize > 5242880)//1024*1024*5)
                        {
                            Response.Write("<script>alert('" + fileitem.FileName + "文件大于5MB')</script>");
                        }
                        else if (dFileSize != 0)
                        {
                            string path = "~/Html/News/" + news.NewsID + "/";
                            //Directory.CreateDirectory(Server.MapPath(path));
                            if (Directory.Exists(Server.MapPath(path)) == false)//如果不存在就创建file文件夹
                            {
                                Directory.CreateDirectory(Server.MapPath(path));
                            }
                            string fFullDir = path + fileitem.FileName;
                            fileitem.SaveAs(Server.MapPath(fFullDir));
                            news.PhotoAddress = fFullDir;
                        }
                    }
                    if (db_news.Update(news))
                        Response.Write("<script>alert('修改成功！ ')</script>");
                    else
                        Response.Write("<script>alert('修改失败！ ')</script>");
                }
            }
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = true;
            return PartialView("List");
        }

        //
        // GET: /News/Details/5
        //新闻细节
        public ActionResult Details(string id = null)
        {
            //News news = db.news.Find(id);
            News news = db_news.SearchbyID(Convert.ToInt32(id));
            if (news == null)
            {
                return HttpNotFound();
            }
            return PartialView(news);
        }
        #endregion

        #region 管理新闻列表
        /// <summary>
        /// llt by 2017.1.9
        /// 列表分页
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult List(int pageIndex = 1)
        {
            List<News> news = db_news.Search();
            pageCount = news.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "News";
            choose = 1;
            //if (Request.IsAjaxRequest()) 
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView();
        }
        //删除列表
        [CheckLogin]
        public ActionResult DeleteList()
        {
            List<News> news = db_news.Search();
            pageCount = news.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "News";
            choose = 2;
            return PartialView();
        }
        #endregion
        #region 新闻主页分页 zsl
        [HttpPost]
        public ActionResult AjaxPagingIndex(int pageIndex, string Type)
        {
            if (Type == "全部" || Type == null)
            {
                List<News> n = db_news.Search(); //数据库取数据
                List<News> news = db_news.SearchPaging(n, pageIndex, pageSize);
                return PartialView("IndexTable", news.ToList());
            }
            else
            {
                List<News> n = db_news.SearchByType(Type); //数据库取数据
                List<News> news = db_news.SearchPaging(n, pageIndex, pageSize);
                return PartialView("IndexTable", news.ToList());
            }
        }
        #endregion
        #region 列表分页
        /// <summary>
        /// 列表分页
        /// add by llt 2017.1.17
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxPaging(int pageIndex = 1)
        {
            List<News> n = db_news.Search(); //数据库取数据
            //List<News> news = new List<News>();
            //for (int i = (pageIndex - 1) * pageSize; i < pageIndex * pageSize; i++)
            //{
            //    if (i == pageCount)
            //        break;
            //    news.Add(n[i]);
            //}
            List<News> news = db_news.SearchPaging(n, pageIndex, pageSize);
            if (choose == 1)
                return PartialView("Table", news.ToList());
            else
                return PartialView("DeleteTable", news.ToList());
        }
        #endregion

        #region 文件上传
        //[HttpPost]
        //public ActionResult uploadHtml(HttpPostedFileBase file)//命名和上传控件name 一样
        //{
        //    var files = Request.Files;
        //    //string type = Request.Form["type"].ToString();
        //    //if (files.Count == 1 && files[0].ContentLength == 0)
        //    if (files.Count == 0 || files[0].ContentLength == 0)
        //    {
        //        ModelState.AddModelError("上传失败，请重新上传", "errorMesaage");
        //        ViewBag.str = "0";
        //        return PartialView("Upload");
        //    }
        //    else
        //    {
        //        #region  执行多个文件上传
        //        for (int i = 0; i < files.Count; i++)
        //        {
        //            HttpPostedFileBase fileitem = files[i];
        //            //判定文件的大小
        //            double dFileSize = fileitem.ContentLength;
        //            if (dFileSize > 5242880)//1024*1024*5)
        //            {
        //                return Content("<script>alert('" + fileitem.FileName + "文件大于5MB')</script>");
        //            }
        //            else
        //            {
        //                //创建文件
        //                string filePath = "~/Html/News/" + db_news.SearchMaxID() + "/";
        //                string FilePath = "/Html/News/" + db_news.SearchMaxID();
        //                Directory.CreateDirectory(Server.MapPath(filePath));
        //                //创建唯一的文件名
        //                string fileName = fileitem.FileName;
        //                string fFullDir = filePath + fileName;
        //                fileitem.SaveAs(Server.MapPath(fFullDir));
        //                StreamReader reader = new StreamReader(Server.MapPath(fFullDir), Encoding.Default);
        //                String a = reader.ReadToEnd();
        //                string name = Path.GetFileNameWithoutExtension(fileName);
        //                string aa = name + ".files";
        //                a = a.Replace(aa, FilePath);
        //                StreamWriter readTxt = new StreamWriter(Server.MapPath(filePath) + "test.html", false, Encoding.Default);
        //                readTxt.Write(a);
        //                readTxt.Flush();
        //                readTxt.Close();
        //                reader.Close();
        //                System.IO.File.Copy(Server.MapPath(filePath) + "test.html", Server.MapPath(fFullDir), true);
        //                System.IO.File.Delete(Server.MapPath(filePath) + "test.html");
        //                News n = db_news.SearchbyID(db_news.SearchMaxID());
        //                n.Coverage = fFullDir;
        //                db.Entry(n).State = EntityState.Modified;
        //                db.SaveChanges();
        //            }
        //        }
        //        #endregion
        //    }
        //    //Session["ID"] = 1;
        //    ViewBag.str = "1";
        //    //return RedirectToAction("UploadTxt");
        //    return PartialView("Upload");
        //}
        public bool UploadTxt(int id, HttpPostedFileBase file)//命名和上传控件name 一样
        {
            //string type = Request.Form["type"].ToString();
            if (file.ContentLength == 0)
            {
                ViewBag.str = "1";
                //ModelState.AddModelError("上传失败，请重新上传", "errorMesaage");
                Response.Write("<script>alert('上传失败，请重新上传！ ')</script>");
                return false;
            }
            else
            {
                //判定文件的大小
                double dFileSize = file.ContentLength;
                if (dFileSize > 5242880)//1024*1024*5)
                {
                    Response.Write("<script>alert('" + file.FileName + "文件大于5MB')</script>");
                    return false;
                }
                else
                {
                    News n = db_news.SearchbyID(id);
                    //创建文件
                    string filePath = "~/Html/News/" + id + "/";
                    Directory.CreateDirectory(Server.MapPath(filePath));
                    //创建唯一的文件名
                    string fileName = file.FileName;
                    string fFullDir = filePath + fileName;

                    string FFullDir = "\\t\\t";
                    file.SaveAs(Server.MapPath(fFullDir));
                    using (StreamReader sr = new StreamReader(Server.MapPath(fFullDir), Encoding.Default))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            FFullDir += line + "\\n\\t\\t";
                        }
                        sr.Close();
                    }
                    n.CoverageTxt = FFullDir;
                    db_news.Update(n);
                    //db.Entry(n).State = EntityState.Modified;
                    //db.SaveChanges();
                }
            }
            return true;
        }
        public bool UploadEnclosure(int id, HttpPostedFileBase file)//命名和上传控件name 一样
        {
            //string type = Request.Form["type"].ToString();
            News n = db_news.SearchbyID(id);
            //判定文件的大小
            double dFileSize = file.ContentLength;
            if (dFileSize == 0)
            {
                return true;
            }
            else if (dFileSize > 5242880)//1024*1024*5)
            {
                Response.Write("<script>alert('上传失败，请重新上传！ ')</script>");
                return false;
            }
            else
            {
                //创建文件
                string filePath = db_news.SearchPath(id);
                string path = "~/Html/News/" + id + "/";
                Directory.CreateDirectory(Server.MapPath(path));
                string fFullDir = path + file.FileName;
                file.SaveAs(Server.MapPath(fFullDir));
                if (string.IsNullOrEmpty(n.PhotoAddress))
                {
                    //zoomauto(Server.MapPath(fFullDir), Server.MapPath(path), 300, 200);

                    n.PhotoAddress = fFullDir;
                    db_news.Update(n);

                    //db.Entry(n).State = EntityState.Modified;
                    //db.SaveChanges();
                }
            }
            //ModelState.AddModelError("上传成功，请继续上传文本内容！", "errorMesaage");
            Response.Write("<script></script>");//alert('上传成功，请继续上传文本内容！ ') 周煜 20017.2.22
            //Session["ID"] = null;
            ViewBag.str = "1";
            return true;
        }

        /// <summary>
        /// 上传html文件
        /// add by llt 2017.2.1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UploadHtm(int id, HttpPostedFileBase fileitem)
        {
            //判定文件的大小
            double dFileSize = fileitem.ContentLength;
            if (dFileSize > 5242880)//1024*1024*5)
            {
                Response.Write("<script>alert('" + fileitem.FileName + "文件大于5MB')</script>");
                return false;
            }
            else
            {
                News n = db_news.SearchbyID(id);
                string filePath = "~/Html/News/" + id + "/";
                string FilePath = "~/Html/News/" + id;
                if (string.IsNullOrEmpty(n.Coverage))
                {
                    //创建文件    
                    if (!Directory.Exists(Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    }
                }
                //创建唯一的文件名
                string fileName = fileitem.FileName;
                string fFullDir = filePath + fileName;
                fileitem.SaveAs(Server.MapPath(fFullDir));
                StreamReader reader = new StreamReader(Server.MapPath(fFullDir), Encoding.Default);
                String a = reader.ReadToEnd();
                string name = Path.GetFileNameWithoutExtension(fileName);
                string aa = name + ".files";
                a = a.Replace(aa, FilePath);
                StreamWriter readTxt = new StreamWriter(Server.MapPath(filePath) + "test.html", false, Encoding.Default);
                readTxt.Write(a);
                readTxt.Flush();
                readTxt.Close();
                reader.Close();
                System.IO.File.Copy(Server.MapPath(filePath) + "test.html", Server.MapPath(fFullDir), true);
                System.IO.File.Delete(Server.MapPath(filePath) + "test.html");

                n.Coverage = fFullDir; //添加替换html路径
                //db.Entry(n).State = EntityState.Modified;
                //db.SaveChanges();
                db_news.Update(n);
                ViewBag.str = "1";

            }
            return true;
        }


        #endregion


        /// <summary>
        /// 绑定新闻类别下拉列表
        /// add by llt 2017.2.2
        /// </summary>
        private void BindDropdown()
        {
            List<Small_Type> news_type = db_stype.Search("新闻"); //取出新闻类别
            listItem_type = new List<SelectListItem>();  //年份下拉框的绑定
            foreach (Small_Type n_type in news_type)
            {
                listItem_type.Add(new SelectListItem { Text = n_type.S_Type, Value = n_type.S_Type });
            }
        }

        [HttpPost]
        public ContentResult ChangePho(HttpPostedFileBase file, string id)//命名和上传控件name 一样
        {
            var files = Request.Files;
            if (files.Count == 0 || files[0].ContentLength == 0)
            {
                ModelState.AddModelError("图片选择失败，请重新上传", "errorMesaage");
                return Content("No");
            }
            else
            {
                HttpPostedFileBase fileitem = files[0];
                //判定文件的大小
                double dFileSize = fileitem.ContentLength;
                if (dFileSize > 5242880)//1024*1024*5)
                {
                    return Content("<script>alert('" + id + "文件大于5MB')</script>");
                }
                else
                {
                    //创建文件
                    //string filePath = db_news.SearchPath(db_news.SearchMaxID());
                    string path = "~/Html/News/" + id + "/";
                    Directory.CreateDirectory(Server.MapPath(path));
                    string fFullDir = path + fileitem.FileName;
                    fileitem.SaveAs(Server.MapPath(fFullDir));
                    //News n = db.news.Find(db_news.SearchMaxID());
                    return Content("<script>$(\"#title\").attr('src', " + fFullDir + ")</script>");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
