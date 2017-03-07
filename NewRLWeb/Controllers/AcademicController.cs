/// <summary>
/// 修改人：周煜
/// 修改内容：1.数据库数据导出
///           2.动态读取相册集照片传入前台
///           
/// </summary>
/// <returns></returns>
/// 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.Package;
using System.IO;
using System.Text;


namespace NewRLWeb.Controllers
{
    public class AcademicController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Logic_Academic_Exchange db_academic = new Logic_Academic_Exchange();
        private readonly int pageSize = 10;
        private static int pageCount;
        private static int? choose;

        #region 页面
        //2016.12.3 zsl
        //public ActionResult HomeIndex()
        //{
        //    //List<Academic_Exchange> academic = db_academic.Search();
        //    //pageCount = academic.Count();
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalCount = db.academic_exchange.Count();
        //    return View();
        //}

        //
        // GET: /Academic/
        //public ActionResult Index(int pageIndex = 1)
        public ActionResult HomeIndex(string time)
        {
             List<Academic_Exchange> aratime;
            if (time == null) 
            { 
             aratime = db_academic.Search(); 
             pageCount = aratime.Count();
             ViewBag.Type = -1;
            
            }
            else
            {
                aratime = db_academic.SearchByTypetime(int.Parse(time));
                pageCount = aratime.Count();
                ViewBag.Type = time;
            }
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            //var persons = (from p in db.academic_exchange orderby p.AcademicName descending select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //return PartialView(persons.ToList());
            return PartialView(aratime);

        }
        //邢亚男 从数据库取数据  2017/02/24
        //public ActionResult Index(int? time)
        //{
        //    List<Academic_Exchange> aratime;
        //    aratime = db_academic.SearchByTypetime(time);
        //    pageCount = aratime.Count();
        //    ViewBag.Type = time;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalCount = pageCount;
        //    return PartialView(aratime);
        //}

        //
        // GET: /Academic/
        public ActionResult Academic_Exchange_View(int id)
        {
            Academic_Exchange academic = db.academic_exchange.Find(id);
            if (academic == null)
            {
                return HttpNotFound();
            }
            return View(academic);
        }
        #endregion

        #region 增 删 改 细节
        //
        // GET: /Academic/Create

        public ActionResult Create()
        {
            return PartialView();
        }
        //
        // POST: /Academic/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        ///周煜 添加学术交流信息 2016 12 8 待测试
        ///卜令梅 修改 2016 12 8
        public ActionResult Create(Academic_Exchange academic_exchange)
        {
            //if (ModelState.IsValid)
            //{
            var fileHtm = Request.Files["UpLoadHtm"];
            var fileEnc = Request.Files["UpLoadEnc"];
            //if (files == null || files.ContentLength == 0)
            //{
            //    Response.Write("<script>alert('文件上传失败，请重新上传！')</script>");
            //    ViewBag.str = "0";
            //}
            //else
            //{
                if (db_academic.HasSameAcdemicExchange(academic_exchange.AcademicName))
                {
                    ModelState.AddModelError("AcademicName", "已有同名");
                    //Response.Write("<script>alert('添加失败！ ')</script>");
                }
                else if ((academic_exchange.BeginTime.Date.CompareTo(DateTime.Now.Date) > 0) || (academic_exchange.EndTime.Date.CompareTo(DateTime.Now.Date) > 0) || (academic_exchange.BeginTime.Date.CompareTo(academic_exchange.EndTime.Date) > 0))
                {
                    Response.Write("<script>alert('时间选择不正确，请重新上传！')</script>");
                }
                else
                {
                    academic_exchange.Coverage = "rtsh";
                    academic_exchange.PhotoAddress = "fhtrh";
                    if (db_academic.Add(academic_exchange))
                    {
                        academic_exchange = db_academic.SearchbyName(academic_exchange.AcademicName);
                        if (UploadHtm(academic_exchange, fileHtm) && UploadEnclosure(academic_exchange, fileEnc))
                        {
                            //fileid = academic_exchange.NewsID;
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
                                    string path = "~/Html/Academics/" + academic_exchange.AcademicID + "/";
                                    //Directory.CreateDirectory(Server.MapPath(path));
                                    //周煜 2017.2.24
                                    //string imgName = "";
                                    //for (int mapLength = 0; mapLength < image.FileName.Length; mapLength++)
                                    //{
                                    //    if (image.FileName[mapLength] == '\\')
                                    //    {
                                    //        imgName = "";
                                    //        continue;
                                    //    }
                                    //    imgName = imgName + image.FileName[mapLength];
                                    //}
                                    string fFullDir = path + "title" + timenow + img.FileName;
                                    image.SaveAs(Server.MapPath(fFullDir));
                                    academic_exchange.PhotoAddress = fFullDir;
                                    db_academic.Update(academic_exchange);
                                }
                            }
                            Response.Write("<script>alert('添加成功,请继续添加附件！')</script>");
                            return PartialView("Upload");
                        }
                        else
                            Response.Write("<script>alert('文件上传失败，请重新上传！')</script>");
                    }
                    else
                        Response.Write("<script>alert('添加失败！')</script>");
                }
            ViewBag.Reload = true;
            ViewBag.Sider = 4;
            return PartialView(academic_exchange);
        }



        public ActionResult Delete(int id)
        {
            Academic_Exchange ae = db_academic.SearchbyId(id);
            if (ae == null)
            {
                return HttpNotFound();
            }
            return PartialView(ae);
        }
        
        //
        // POST: /Academic/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (db_academic.Delete(id))
                return RedirectToAction("List");
            else
            {
                Response.Write("<script>alert('删除失败！')</script>");
                Academic_Exchange ae = db_academic.SearchbyId(id);
                return PartialView("Delete", ae);
            }
        }

        [HttpPost]
        public ActionResult MultDelete(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL;
            {
                string strids = collection.GetValue("checks").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                ids = strids.Split(',');
                string num = ""; //检查是否删除勾选的
                foreach (string getid in ids)
                {
                    int ID = Convert.ToInt32(getid);
                    if (!db_academic.Delete(ID))
                        num = num + getid + " ";
                }
                if (num.Length > 0)
                    Response.Write("<script>alert('" + num + "删除失败！')</script>");
                else
                    Response.Write("<script>alert('删除成功！')</script>");
            }
            else
                Response.Write("<script>alert('无选中！')</script>");
            return RedirectToAction("DeleteList");
        }

        //
        // GET: /Academic/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Academic_Exchange academic_exchange = db_academic.SearchbyId(id);
            if (academic_exchange == null)
            {
                return HttpNotFound();
            }
            return PartialView(academic_exchange);
        }

        //
        // POST: /Academic/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Academic_Exchange academic_exchange)
        {
            if (ModelState.IsValid)
            {
                if (db_academic.HasSameAcdemicExchange(academic_exchange))
                {
                    ModelState.AddModelError("AcademicName", "已有同名新闻");
                    //Response.Write("<script>alert('添加失败！ ')</script>");
                }
                else if ((academic_exchange.BeginTime.Date.CompareTo(DateTime.Now.Date) > 0) || (academic_exchange.EndTime.Date.CompareTo(DateTime.Now.Date) > 0) || (academic_exchange.BeginTime.Date.CompareTo(academic_exchange.EndTime.Date) > 0))
                {
                    Response.Write("<script>alert('时间选择不正确，请重新上传！')</script>");
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
                            string timenow = DateTime.Now.ToString("yyyyMMddhhmmss");//得到系统时间
                            string path = "~/Html/Academics/" + academic_exchange.AcademicID + "/";
                            //Directory.CreateDirectory(Server.MapPath(path));
                            string fFullDir = path + "title" + timenow + fileitem.FileName;
                            fileitem.SaveAs(Server.MapPath(fFullDir));
                            academic_exchange.PhotoAddress = fFullDir;
                        }
                    }
                    if (db_academic.Update(academic_exchange))
                        Response.Write("<script>alert('修改成功！ ')</script>");
                    else
                        Response.Write("<script>alert('修改失败！ ')</script>");
                }
            }
            else
                Response.Write("<script>alert('修改失败！ ')</script>");
            ViewBag.Reload = true;
            ViewBag.Sider = 4;
            return PartialView(academic_exchange);
        }

        public ActionResult Details(int id = 0)
        {
            Academic_Exchange academic_exchange = db.academic_exchange.Find(id);
            if (academic_exchange == null)
            {
                return HttpNotFound();
            }
            return PartialView(academic_exchange); ;
        }

        #endregion

        #region 管理学术交流会列表
        public ActionResult List(int pageIndex = 1)
        {
            List<Academic_Exchange> ae = db_academic.Search();
            pageCount = ae.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Academic";
            if (choose != null && choose == 3)
            {
                ViewBag.Reload = true;
                ViewBag.Sider = 4;
            }
            choose = 1;
            return PartialView();
        }

        public ActionResult DeleteList()
        {
            List<Academic_Exchange> ae = db_academic.Search();
            pageCount = ae.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Academic";
            choose = 2;
            return PartialView("DeleteList");
        }
        /// <summary>
        /// 分页
        /// add by llt 2017.2.11
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxPaging(int pageIndex = 1)
        {
            List<Academic_Exchange> n = db_academic.Search();
            List<Academic_Exchange> ae = db_academic.SearchPaging(n, pageIndex, pageSize);
            if (choose == 1)
                return PartialView("Table", ae.ToList());
            else
                return PartialView("DeleteTable", ae.ToList());
        }
        #endregion

        #region 文件上传

        //public bool uploadHtml(int id,HttpPostedFileBase file)//命名和上传控件name 一样
        //{
        //    if (file.ContentLength == 0)
        //    {
        //        ModelState.AddModelError("上传失败，请重新上传", "errorMesaage");
        //        ViewBag.str = "0";
        //        return false;
        //    }
        //    else
        //    {
        //            //判定文件的大小
        //            double dFileSize = file.ContentLength;
        //            if (dFileSize > 5242880)//1024*1024*5)
        //            {
        //               Response.Write("<script>alert('" + file.FileName + "文件大于5MB')</script>");
        //               return false;
        //            }
        //            else
        //            {
        //                //创建文件
        //                string filePath = "~/Html/Academics/" + id + "/";
        //                string FilePath = "~/Html/Academics/" + id;
        //                Directory.CreateDirectory(Server.MapPath(filePath));
        //                //创建唯一的文件名
        //                string fileName = file.FileName;
        //                string fFullDir = filePath + fileName;
        //                file.SaveAs(Server.MapPath(fFullDir));
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
        //                Academic_Exchange n = db.academic_exchange.Find(id);
        //                n.Coverage = fFullDir;
        //                db.Entry(n).State = EntityState.Modified;
        //                db.SaveChanges();
        //            }
        //       }
        //    //Session["ID"] = 1;
        //    ViewBag.str = "1";
        //    //return RedirectToAction("UploadTxt");
        //    return true;
        //}
        public bool UploadEnclosure(Academic_Exchange ae, HttpPostedFileBase file)//命名和上传控件name 一样
        {
                    //判定文件的大小
                    double dFileSize = file.ContentLength;
                    if (dFileSize > 5242880)//1024*1024*5)
                    {
                        Response.Write("<script>alert('" + file.FileName + "文件大于5MB')</script>");
                    }
                    else
                    {
                        //创建文件
                        string filePath = "~/Html/Academics/" + ae.AcademicID + "/";
                        Directory.CreateDirectory(Server.MapPath(filePath));
                        //周煜 2017.2.24 截取文件名
                        string documentName = "";
                        for (int mapLength = 0; mapLength < file.FileName.Length; mapLength++)
                        {
                            if (file.FileName[mapLength] == '\\')
                            {
                                documentName = "";
                                continue;
                            }
                            documentName = documentName + file.FileName[mapLength];
                        }
                        string fFullDir = filePath + documentName;
                        file.SaveAs(Server.MapPath(fFullDir));
                            //zoomauto(Server.MapPath(fFullDir), Server.MapPath(path), 300, 200);
                            Academic_Exchange n = db.academic_exchange.Find(ae.AcademicID);
                            if (string.IsNullOrEmpty(n.PhotoAddress))
                            {
                                n.PhotoAddress = fFullDir;
                                db_academic.Update(n);
                                //db.Entry(n).State = EntityState.Modified;
                                //db.SaveChanges();
                            }
                    }
            //Session["ID"] = null;
            choose = 3;
            return true;
        }

        /// <summary>
        /// 上传html文件
        /// add by llt 2017.2.11
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UploadHtm(Academic_Exchange ae, HttpPostedFileBase fileitem)
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
                string filePath = "~/Html/Academics/" + ae.AcademicID + "/";
                string FilePath = "/Html/Academics/" + ae.AcademicID;
                //创建文件    
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
                //创建唯一的文件名
                //周煜 2017.2.24  文件名截取
                string fileName = fileitem.FileName;
                string path = "";
                for (int mapLength = 0; mapLength < fileitem.FileName.Length; mapLength++)
                {
                    if (fileitem.FileName[mapLength] == '\\')
                    {
                        path = "";
                        continue;
                    }
                    path = path + fileitem.FileName[mapLength];
                }
                string fFullDir = filePath + path;
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

                ae.Coverage = fFullDir; //添加替换html路径
                db_academic.Update(ae);
                ViewBag.str = "1";
                return true;
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #region 学术交流分页 gq
        [HttpPost]
        public ActionResult AjaxPagingIndex(int pageIndex,string time)
        {
        if(time==null||time=="-1")
        {
            List<Academic_Exchange> n = db_academic.Search();
            List<Academic_Exchange> ae = db_academic.SearchPaging(n, pageIndex, pageSize);
            return PartialView("IndexTable", ae.ToList());
        }
        else
        {
            List<Academic_Exchange> n = db_academic.SearchByTypetime(int.Parse(time));
            List<Academic_Exchange> ae = db_academic.SearchPaging(n, pageIndex, pageSize);
            return PartialView("IndexTable", ae.ToList());
          
}
        }
        #endregion
    }
        

}
