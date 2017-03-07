using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NewRLWeb.Models;
using NewRLWeb.Package;
using System.Text;
using System.IO;
using NewRLWeb.ViewCode;
using NewRLWeb.ViewModels;
using NewRLWeb.Filters;

/// <summary>
/// 修改人：夏禹
/// 修改内容：1.数据库数据迁入
///           2.动态读取相册集照片传入前台
/// 修改人：周煜
/// 修改内容：1.把图片存入项目中
///           2.获取前台的数据
/// </summary>
/// <returns></returns>
/// 

namespace NewRLWeb.Controllers
{
    public class AlbumController : Controller
    {
        private Logic_Album db_album = new Logic_Album();
        private Logic_Photos db_photos = new Logic_Photos();
        private rlwzContext db = new rlwzContext();
        private vc_Album vc = new vc_Album();
        private Logic_Small_Type db_stype = new Logic_Small_Type();
        private static List<SelectListItem> listItem_type;
        private readonly int pageSize = 6;  //分页
        private static int pageCount;
        private static int choose;

        #region 相册前台
        /// <summary>
        /// 评论上传
        /// 编写人：夏禹
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult DealComment(string content)
        {
            ViewBag.b = content;
            return View();
        }
        /// <summary>
        /// 显示相册列表
        /// 编写人夏禹
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string Type)
        {
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string jsonStr = js.Serialize(db.album.ToList());
            //ViewBag.Album = jsonStr;
            //int pageScoll = 0;
            //List<Album> albums = db_album.Search().Skip(pageScoll * albumNum).Take(albumNum).ToList();
            if (Type == "全部" || Type == null)
            {
                List<Album> a = db_album.Search();
                pageCount = a.Count();
                ViewBag.Title = "全部";
                ViewBag.PageSize = pageSize;
                ViewBag.TotalCount = pageCount;
                return View(a);
            }
            else
            {
                List<Album> a = db_album.Search(Type);
                pageCount = a.Count();
                ViewBag.Title = Type;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalCount = pageCount;
                return View(a);
            }
        }

        /// <summary>
        /// 显示对应相册的全部图片
        /// 编写人：夏禹
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Photos(int id, string name) //name表示相册名 type表示相册种类
        {
            ViewBag.AlbumName = name;
            var ls =
                 from a in db.photos
                 where a.AlbumID == id
                 select a;
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonStr = js.Serialize(ls);
            ViewBag.temp = jsonStr;
            return View("Photos");
        }

        #endregion

        #region  周煜2016.11.23 相册
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                db.album.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(album);
        }
        #endregion

        #region 相册管理列表
        [CheckLogin]
        public ActionResult List(int pageIndex = 1)
        {
            List<Album> Album = db_album.Search();
            pageCount = Album.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Album";
            if (choose == 3)
            {
                ViewBag.Reload = true;
                ViewBag.Sider = 2;
            }
            choose = 1;
            //if (Request.IsAjaxRequest()) 
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView();
        }
        [CheckLogin]
        public ActionResult DeleteList()
        {
            List<Album> album = db_album.Search();
            pageCount = album.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Album";
            choose = 2;
            //if (Request.IsAjaxRequest()) 
            return PartialView();
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
            List<Album> a = db_album.Search();
            List<Album> albums = db_album.SearchPaging(a, pageIndex, pageSize);
            //for (int i = (pageIndex - 1) * pageSize; i < pageIndex * pageSize; i++)
            //{
            //    if (i == pageCount)
            //        break;
            //    albums.Add(a[i]);
            //}
            if (choose == 1)
                return PartialView("Table", albums.ToList());
            else
                return PartialView("DeleteTable", albums.ToList());
        }

        [HttpPost]
        public ActionResult IndexPaging(int pageIndex, string Type)
        {
            if (Type == "全部" || Type == null)
            {
                List<Album> a = db_album.Search();
                List<Album> albums = db_album.SearchPaging(a, pageIndex, pageSize);
                return PartialView("IndexPhoto", albums.ToList());
            }
            else
            {
                List<Album> a = db_album.Search(Type);
                List<Album> albums = db_album.SearchPaging(a, pageIndex, pageSize);
                return PartialView("IndexPhoto", albums.ToList());
            }
        }
        #endregion

        #region 增 删 改 细节
        //
        // GET: /Album/Create
        // 增加视图
        [CheckLogin]
        public ActionResult Create()
        {
            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            // using (rlwzContext db = new rlwzContext() { })
            return PartialView();
        }

        //创建个人相册
        //编写人：夏禹
        public ActionResult CreatePersonAblum(IEnumerable<HttpPostedFileBase> files, string cancelImg, string UniqueID)
        {
            //  using logic_
            try
            {
                db_album.SearchFromName(UniqueID);
            }
            catch
            {
                vc.CreatePersonAlbum(UniqueID);
            }

            PhotoUpload(files, cancelImg, UniqueID);
            Response.Write("<script>alert('添加成功！ ')</script>");
            return RedirectToAction("Index", "Users");
        }
        //
        // POST: /Album/Create
        // 增加相册
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album, IEnumerable<HttpPostedFileBase> files, IEnumerable<HttpPostedFileBase> xdaTanFileImg)
        {
            album.Publicationtime = DateTime.Now;
            var f = Request.Files["xdaTanFileImg"];
            if (db_album.HasAlbumName(album.Albumname))
            {
                ModelState.AddModelError("Albumname", "已有同名相册");
            }
            else if (f != null && f.ContentLength > 0)
            {
                string timenow = DateTime.Now.ToString("yyyyMMddhhmmss");//得到系统时间
                string path = Server.MapPath("/Images/Album/" + album.Albumname);
                album.Folderpath = "/Images/Album/" + album.Albumname;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                HttpPostedFileBase file = f; //Request.f["f"];
                string fFullDir = Server.MapPath("/Images/Album/") + album.Albumname + "/title" + timenow + System.IO.Path.GetFileName(file.FileName);
                file.SaveAs(fFullDir);
                album.Pho_Address = album.Folderpath + "/title" + timenow + System.IO.Path.GetFileName(file.FileName);

                db_album.Create(album);
                ViewBag.Reload = true;
                ViewBag.Sider = 2;
            }
            else
            {
                Response.Write("<script>alert('请添加相册展示图片！ ')</script>");
            }
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = true;
            ViewBag.Sider = 2;
            MultiUpload(files);
            return RedirectToAction("List");
        }

        /// <summary>
        /// 添加图片视图
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public ActionResult AddPhotos(Album album)
        {
            return PartialView(album);
        }
        /// <summary>
        /// 相册图片添加
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultiUpload(IEnumerable<HttpPostedFileBase> files)
        {
            Photos newPhoto = new Photos();
            string cancelImg = Request["txt1"];
            string name = Request.Form["Albumname"];
            Album album = db_album.SearchFromName(name);
            int j = 0;
            int countt = 0;
            List<int> intArr = new List<int>();
            int count = 0;
            int count_1 = 0;
            if (cancelImg != null)
            {
                for (int i = 0; i < cancelImg.Length; i++)
                {
                    if (cancelImg[i] == '/')
                    {
                        count++;
                    }
                }
                string[] arrStr = new string[count];
                for (int i = 0; i < cancelImg.Length; i++)
                {
                    if (cancelImg[i] == '/')
                    {
                        count_1++;
                    }
                    else
                    {
                        arrStr[count_1] += cancelImg[i].ToString();
                    }
                }
                for (int i = 0; i < count_1; i++)
                {
                    intArr.Add(int.Parse(arrStr[i]));
                }
            }
            intArr.Sort();//得到一个升序的数组,共有count_1个有效元素，以及下面有一个结尾元素;
            intArr.Add(1321456);
            int ttttt = 0;
            ////////////////////////////////////////////////////////////////////////////////////
            string realPath = "/Images/Album/" + album.Albumname;
            string pathForSaving = Server.MapPath(realPath);
            if (this.CreateFolderIfNeeded(pathForSaving))
            {
                foreach (var file in files)
                {
                    if (countt < count_1)
                    {
                        if (intArr[countt] - 1 == j)
                        {
                            countt++;
                            goto RE;
                        }
                    }
                    if (file != null && file.ContentLength > 0)
                    {
                        DateTime dt = DateTime.Now;

                        string realName = dt.ToFileTime().ToString() + file.FileName;
                        var path = Path.Combine(pathForSaving, realName);
                        ///这里ID有点问题
                        newPhoto.Address = "~" + realPath + realName;
                        newPhoto.AlbumID = album.AlbumID;
                        newPhoto.L_Type = album.Album_Type;
                        db.photos.Add(newPhoto);
                        db.SaveChanges();
                        ttttt++;
                        file.SaveAs(path);
                    }
                RE: j++;
                    continue;
                }
            }
            choose = 3;
            return RedirectToAction("List");
        }

        //个人相册图片上传
        [HttpPost]
        public void PhotoUpload(IEnumerable<HttpPostedFileBase> files, string cancelImg, string UniqueID)
        {
            string name = UniqueID;
            //CreatePersonAblum(name);
            Photos newPhoto = new Photos();

            Album album = db_album.SearchFromName(name);

            int j = 0;
            int countt = 0;
            List<int> intArr = new List<int>();
            int count = 0;
            int count_1 = 0;
            int count_2 = 0;
            if (cancelImg != null)
            {
                for (int i = 0; i < cancelImg.Length; i++)
                {
                    if (cancelImg[i] == '/')
                    {
                        count++;
                    }
                }
                string[] arrStr = new string[count];
                for (int i = 0; i < cancelImg.Length; i++)
                {
                    if (cancelImg[i] == '/')
                    {
                        count_1++;
                    }
                    else
                    {
                        arrStr[count_1] += cancelImg[i].ToString();
                    }
                }
                for (int i = 0; i < count_1; i++)
                {
                    intArr.Add(int.Parse(arrStr[i]));
                }
            }
            intArr.Sort();//得到一个升序的数组,共有count_1个有效元素，以及下面有一个结尾元素;
            intArr.Add(1321456);
            int ttttt = 0;
            ////////////////////////////////////////////////////////////////////////////////////
            // string realPath = "/Images/Album/" + album.Albumname;
            string realPath = album.Folderpath;
            string pathForSaving = Server.MapPath(realPath);
            if (this.CreateFolderIfNeeded(pathForSaving))
            {
                foreach (var file in files)
                {
                    if (countt < count_1)
                    {
                        if (intArr[countt] - 1 == j)
                        {
                            countt++;
                            goto RE;
                        }
                    }
                    if (file != null && file.ContentLength > 0)
                    {
                        DateTime dt = DateTime.Now;
                        string realName = dt.ToFileTime().ToString() + file.FileName;
                        var path = Path.Combine(pathForSaving, realName);
                        ///这里ID有点问题
                        if (ModelState.IsValid)
                        {
                            newPhoto.Address = realPath + "/" + realName;

                            newPhoto.AlbumID = album.AlbumID;
                            newPhoto.L_Type = album.Album_Type;
                            db.photos.Add(newPhoto);
                            db.SaveChanges();
                            if (count_2 == 0)
                            {

                                //      album.A_PhotoID = db_photos.SearchFormAdress(realPath + realName);
                                album.Pho_Address = realPath + realName;
                                db_album.Update(album);
                                count_2++;
                            }
                            ttttt++;
                        }
                        file.SaveAs(path);
                    }
                RE: j++;
                    continue;
                }
            }


        }
        public ActionResult AddPicters(int id)
        {
            Album album = db_album.SearchFromAlbumID(id);
            return PartialView("AddPhotos", album);
        }

        //
        // GET: /Album/Delete/5
        /// <summary>
        /// 删除相册
        /// 编写人：夏禹
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Album album = db_album.SearchFromAlbumID(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return PartialView(album);
        }
        // POST: /Album/Delete/5
        // 确定删除相册
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComfirmed(int id)
        {
            Album al = db_album.SearchFromAlbumID(id);
            string path = Server.MapPath(al.Folderpath);
            DirectoryInfo di = new DirectoryInfo(path);
            if (db_album.Delete(id))
            {
                di.Delete(true);
                db_photos.DeleteFromAlbumID(id);
                return PartialView("DeleteList");
            }
            else
            {
                Response.Write("<script>alert('删除失败！ ')</script>");
                Album album = db_album.SearchFromAlbumID(id);
                return PartialView("Delete", album);
            }

        }
        /// <summary>
        /// 相册删除视图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultDelete(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL
            {
                string strids = collection.GetValue("checks").AttemptedValue;//AttemptedValue返回一个以","分割的字符串
                ids = strids.Split(',');
                foreach (string getid in ids)
                {
                    int ID = Convert.ToInt32(getid);
                    Album al = db_album.SearchFromAlbumID(ID);
                    string path = Server.MapPath(al.Folderpath);//"/Images/Album/" + al.Albumname);
                    DirectoryInfo di = new DirectoryInfo(path);
                    di.Delete(true);
                    db_album.Delete(ID);
                    db_photos.DeleteFromAlbumID(ID);
                }
            }
            return RedirectToAction("DeleteList");
        }


        //
        // GET: /Album/Edit/5
        // 相册编辑视图
        public ActionResult Edit(int id)
        {
            //Album album = db.album.Find(id);
            Album album = db_album.SearchFromAlbumID(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            BindDropdown();
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            return PartialView(album);
        }
        /// <summary>
        /// 修改相册信息
        /// 编写人夏禹
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                if (db_album.HasAlbumName(album.AlbumID, album.Albumname))
                {
                    ModelState.AddModelError("Albumname", "已有同名相册");
                    //Response.Write("<script>alert('添加失败！ ')</script>");
                }
                else if (album.Publicationtime.CompareTo(DateTime.Now) > 0)
                {
                    ModelState.AddModelError("Publicationtime", "发布日期超出当前时间");
                }
               
                else
                {
                    HttpPostedFileBase file = Request.Files["xdaTanFileImg"];
                    if(file!=null && file.ContentLength>0)
                    {
                     string ablumpath = Server.MapPath("/Images/Album/" + album.Albumname) ;//
                     string path = Server.MapPath("/Images/Album/" + album.Albumname+"/"+file.FileName);
                     if (!Directory.Exists(ablumpath))//判断是否存在
        {
            Directory.CreateDirectory(ablumpath);//创建新路径
        }
                      file.SaveAs(path);
                      album.Pho_Address = "/Images/Album/" + album.Albumname + "/" + file.FileName;
                      Photos p = new Photos();
                      p.Address = "/Images/Album/" + album.Albumname + "/" + file.FileName;
                      p.AlbumID =album.AlbumID;
                      p.L_Type = album.Album_Type;
                      db.photos.Add(p);
                    } 
                    if (db_album.Update(album))
                    {
                        choose = 3;
                        return RedirectToAction("List");
                    }

                    else
                        Response.Write("<script>alert('添加失败!')</script>");
                }
                //Album al = db_album.SearchFromAlbumID(album.AlbumID);
                //string path = Server.MapPath("/Images/Album/" + al.Albumname);
                //DirectoryInfo di = new DirectoryInfo(path);
                //string newPath = Server.MapPath("/Images/Album/" + album.Albumname);
                //if (db_album.ChangeAlbumImfo(album))
                //    return RedirectToAction("List");
                ////if (db_album.HasAlbumName(album.Albumname))
                ////{
                ////    ModelState.AddModelError("Album", "已有同名相册");
                ////}
                ////else
                ////{
                //    di.MoveTo(newPath);
              
 
            }
            ViewData["List1"] = new SelectList(listItem_type, "Value", "Text");
            ViewBag.Reload = true;
            return PartialView(album);
        }

        /// <summary>
        ///相册中照片的修改（增删改查）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeAlbumDetail(int id = 2)
        {
            return View(db_photos.searchAlbumPhotos(id));
        }


        //
        // GET: /Album/Details/5
        // 相册细节
        public ActionResult Details(int id = 0)
        {
            Album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return PartialView(album);
        }
        #endregion

        #region 检查是否要创建上传文件夹
        private bool CreateFolderIfNeeded(string filePath)
        {
            bool result = true;
            if (!Directory.Exists(filePath))//如果不存在要存储的文件夹，则创建相应文件夹
            {
                try
                {
                    Directory.CreateDirectory(filePath);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
        #endregion

        #region 获取评论
        private Comment model = new Comment();

        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public ActionResult GetComment(int TypeID,string Type)
        {
            try
            {
                List<ViewModels.vm_Comment> vm = new List<vm_Comment>();
                vm = model.GetComment(TypeID,Type);
                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private static int pageScoll = 0;
        /// <summary>
        /// 图片向下加载拉伸
        /// add by llt 2017.1.19
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxScoll()
        {
            pageScoll += 1;
            List<Album> query = (from o in db.album
                                 orderby o.Publicationtime
                                 select o).Skip(pageScoll * pageSize).Take(pageSize).ToList<Album>();
            JsonResult jt = Json(query.ToArray(), JsonRequestBehavior.AllowGet);
            jt.ContentType = "text/html";
            return jt;
        }

        /// <summary>
        /// 绑定相册类别下拉列表
        /// add by llt 2017.2.5
        /// </summary>
        private void BindDropdown()
        {
            List<Small_Type> Album_type = db_stype.SearchNewsType("相册");
            listItem_type = new List<SelectListItem>();
            foreach (Small_Type a_type in Album_type)
            {
                listItem_type.Add(new SelectListItem { Text = a_type.S_Type, Value = a_type.S_Type });
            }
        }

        /// <summary>
        /// 局部显示相册图片
        /// 编写人：夏禹
        /// </summary>
        /// <param name="AlbumID"></param>
        /// <returns></returns>
        public ActionResult SelectPhoto(int AlbumID = 2)
        {
            return View(db_photos.searchAlbumPhotos(AlbumID));
        }

        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = "/Upload/" + filePathName
            });

        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}