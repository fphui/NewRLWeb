using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;
using NewRLWeb.ViewCode;
using NewRLWeb.Package;
using System.IO;
using System.Text;
using NewRLWeb.Filters;

namespace NewRLWeb.Controllers
{
    public class ProjectController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Project pj = new Project();
        private Logic_Project_Case db_project = new Logic_Project_Case();
        private Logic_Small_Type db_stype = new Logic_Small_Type();
        private static List<SelectListItem> listItem_type;
        private readonly int pageSize = 10;
        private static int pageCount;
        private static int? choose;

        #region 项目案例页面
        //
        // GET: /Project/
        //邢亚男  2017/02/22  数据库传数据
        public ActionResult Index(string type)
        {
            List<Project_Case> project = db_project.SearchByType(type);
            pageCount = project.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            //ViewBag.Type = type;
            return View(project);
        }

        //
        // GET: /Project/

        public ActionResult ProjectView(int id)
        {
            ProjectCase project_case = pj.search(id);
            if (project_case == null)
            {
                return HttpNotFound();
            }
            return View(project_case);
        }
        #endregion

        #region 管理项目案例 增 删 改 细节
        //
        // GET: /Project/Create
        // 项目增加页面
        [CheckLogin]
        public ActionResult Create()
        {
            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = false;
            return PartialView();
        }
        //
        // POST: /Project/Create
        // 项目增加
        [HttpPost]
        [ValidateAntiForgeryToken]
        //周煜 2017.2.23 注释掉部分的<script>alert()</script>
        public ActionResult Create(Project_Case project_case)
        {
            if (ModelState.IsValid)
            {
                var fileHtm = Request.Files["UpLoadHtm"];
                var fileEnc = Request.Files["UpLoadEnc"];
                    if (db_project.HasSameProject(project_case.Projectname))
                    {
                        ModelState.AddModelError("Projectname", "已有同名项目");
                        //Response.Write("<script>alert('添加失败！ ')</script>");
                    }
                    else if ((project_case.Startdate.Date.CompareTo(DateTime.Now.Date) > 0) || (project_case.Enddate.Date.CompareTo(DateTime.Now.Date) > 0) || (project_case.Startdate.Date.CompareTo(project_case.Enddate.Date) > 0))
                    {
                        Response.Write("<script>alert('时间选择不正确，请重新上传！')</script>");
                    }
                    else
                    {
                        if (db_project.Add(project_case))
                        {
                            project_case = db_project.SearchbyName(project_case.Projectname);
                            if (UploadHtm(project_case, fileHtm)&&UploadEnclosure(project_case,fileEnc))
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
                                        string path = "~/Html/Project/" + project_case.ProjectID + "/";
                                        //Directory.CreateDirectory(Server.MapPath(path));
                                        ///////////////////////////周煜 文件名截取/////////////////////////////////////////////////////
                                        string imgName = "";
                                        for (int mapLength = 0; mapLength < image.FileName.Length; mapLength++)
                                        {
                                            if (image.FileName[mapLength] == '\\')
                                            {
                                                imgName = "";
                                                continue;
                                            }
                                            imgName = imgName + image.FileName[mapLength];
                                        }
                                        //string fFullDir = path + "title" + timenow + image.FileName;
                                        string fFullDir = path + "title" + timenow + imgName;
                                        ///////////////////////////////////////////////////////////////////////////////////
                                        image.SaveAs(Server.MapPath(fFullDir));
                                        project_case.PhotoAddress = fFullDir;
                                        db_project.Update(project_case);
                                    }
                                }
                                Response.Write("<script>alert('添加成功,请继续添加附件！')</script>");
                                return PartialView("Upload");
                            }
                            else
                                //Response.Write("<script>alert('文件上传失败，请重新上传！')</script>");
                                Response.Write("<script></script>");
                        }
                        else
                            //Response.Write("<script>alert('添加失败！')</script>");
                            Response.Write("<script></script>");
                    }
            }
            else
                //Response.Write("<script>alert('添加失败！')</script>");
                Response.Write("<script></script>");
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = true;
            ViewBag.Sider = 5;
            return PartialView(project_case);
        }

        //
        // GET: /Project/Delete/5
        // 项目删除页面
        public ActionResult Delete(int id = 0)
        {
            Project_Case project_case = db_project.SearchbyID(id);
            if (project_case == null)
            {
                return HttpNotFound();
            }
            return PartialView(project_case);
        }
        //
        // POST: /Project/Delete/5
        // 项目删除
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (db_project.Delete(id))
                return RedirectToAction("List");
            else
            {
                Response.Write("<script>alert('删除失败！')</script>");
                Project_Case project = db_project.SearchbyID(id);
                return PartialView("Delete", project);
            } 
        }
        /// <summary>
        /// 多案例删除
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
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
                    if (!db_project.Delete(ID))
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
        // GET: /Project/Edit/5
        // 项目编辑页面
        public ActionResult Edit(int id = 0)
        {
            Project_Case project_case = db_project.SearchbyID(id);
            if (project_case == null)
            {
                return HttpNotFound();
            }
            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            return PartialView(project_case);
        }
        //
        // POST: /Project/Edit/5
        // 项目编辑
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project_Case project_case)
        {
            if (ModelState.IsValid)
            {
                if (db_project.HasSameProject(project_case))
                {
                    ModelState.AddModelError("Projectname", "已有同名项目");
                }
                else if ((project_case.Startdate.Date.CompareTo(DateTime.Now.Date) > 0) || (project_case.Enddate.Date.CompareTo(DateTime.Now.Date) > 0) || (project_case.Startdate.Date.CompareTo(project_case.Enddate.Date) > 0))
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
                            string path = "~/Html/Project/" + project_case.ProjectID + "/";
                            //Directory.CreateDirectory(Server.MapPath(path));
                            string fFullDir = path + "title" + timenow + fileitem.FileName;
                            fileitem.SaveAs(Server.MapPath(fFullDir));
                            project_case.PhotoAddress = fFullDir;
                        }
                    }
                    if (db_project.Update(project_case))
                    {
                        Response.Write("<script>alert('修改成功！ ')</script>");
                        return RedirectToAction("List");
                    }
                    else
                        Response.Write("<script>alert('修改失败！ ')</script>");
                }
            }
            else
                Response.Write("<script>alert('修改失败！ ')</script>");
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = true;
            ViewBag.Sider = 5;
            return PartialView(project_case);
        }

        //
        // GET: /Project/Details/5
        // 项目案例细节
        public ActionResult Details(int id)
        {
            Project_Case project_case = db_project.SearchbyID(id);
            if (project_case == null)
            {
                return HttpNotFound();
            }
            return PartialView(project_case);
        }
        #endregion

        #region 管理项目案例列表
        [CheckLogin]
        public ActionResult List(int pageIndex = 1)
        {
            List<Project_Case> p= db_project.Search();
            pageCount = p.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Project";
            if (choose != null && choose == 3)
            {
                ViewBag.Reload = true;
                ViewBag.Sider = 5;
            }

            choose = 1;
            //if (Request.IsAjaxRequest()) 
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView();
        }
        [CheckLogin]
        public ActionResult DeleteList()
        {
            //if (Session["DeleteStation"] != null)
            //{
            //    if ((bool)Session["DeleteStation"])
            //        Response.Write("<script>alert('删除成功！')</script>");
            //    else
            //        Response.Write("<script>alert('删除失败！')</script>");
            //}
            List<Project_Case> p= db_project.Search();
            pageCount =p.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Project";
            choose = 2;
            //if (Request.IsAjaxRequest()) 
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView("List");
        }
        /// <summary>
        /// 分页
        /// add by llt 2017.1.17
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxPaging(int pageIndex = 1)
        {
            List<Project_Case> n =  db_project.Search();
            List<Project_Case> projects = db_project.SearchPaging(n, pageIndex, pageSize);
            if (choose == 1)
                return PartialView("Table", projects.ToList());
            else
                return PartialView("DeleteTable", projects.ToList());
        }
        #endregion
      
        #region 文件上传
        public bool UploadEnclosure(Project_Case project, HttpPostedFileBase file)//命名和上传控件name 一样
        {
                    //判定文件的大小
                    double dFileSize = file.ContentLength;
                    if (dFileSize > 5242880)//1024*1024*5)
                    {
                        Response.Write("<script>alert('上传失败，请重新上传！ ')</script>");
                        return false;
                    }
                    else
                    {
                        //创建文件
                        string filePath = "~/Html/Project/" + project.ProjectID + "/";
                        Directory.CreateDirectory(Server.MapPath(filePath));
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
                            Project_Case n = db.project_case.Find(project.ProjectID);
                            n.PhotoAddress = fFullDir;
                            if (string.IsNullOrEmpty(n.PhotoAddress))
                                db_project.Update(n);
                            //db.Entry(n).State = EntityState.Modified;
                            //db.SaveChanges();
                    }
            //Session["ID"] = null;
            choose = 3;
            return true;
        }

        /// <summary>
        /// 上传html文件
        /// add by llt 2017.2.12
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UploadHtm(Project_Case project, HttpPostedFileBase fileitem)
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
                string filePath = "~/Html/Project/" + project.ProjectID + "/";
                string FilePath = "/Html/Project/" + project.ProjectID;
                //创建文件    
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
                //创建唯一的文件名
                string fileName = fileitem.FileName;
                string path="";
                for (int mapLength = 0; mapLength < fileName.Length; mapLength++)
                {
                    if(fileName[mapLength]=='\\'){
                        path = "";
                        continue;
                    }
                    path = path + fileName[mapLength];
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

                project.Coverage = fFullDir; //添加替换html路径
                db_project.Update(project);
                ViewBag.str = "1";
                return true;
            }
        }

         #endregion


        /// <summary>
        /// 绑定项目类别下拉列表
        /// add by llt 2017.2.2
        /// </summary>
        private void BindDropdown()
        {
            List<Small_Type> project_type = db_stype.Search("项目"); //取出项目类别
            listItem_type = new List<SelectListItem>();  //年份下拉框的绑定
            foreach (Small_Type p_type in project_type)
            {
                listItem_type.Add(new SelectListItem { Text = p_type.S_Type, Value = p_type.S_Type });
            }
        }
        #region 分页
        [HttpPost]
        /*邢亚男  2017/02/22  Projec分页数据库读取数据*/
        public ActionResult AjaxPagingIndex(int pageIndex = 1)
        {
            List<Project_Case> n = db_project.Search(); //数据库取数据
            List<Project_Case> project = db_project.SearchPaging(n, pageIndex, pageSize);
            return PartialView("IndexTable", project.ToList());
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}