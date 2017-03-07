using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;
using NewRLWeb.Package;
using NewRLWeb.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Data;
using System.Collections.Generic;
using NewRLWeb.Filters;
using System.Web.Script.Serialization;



namespace NewRLWeb.Controllers
{
    public class UsersController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Logic_Users db_user = new Logic_Users();
        private Logic_Photos photo = new Logic_Photos();
        private UserAuthorizeAttribute user_aa = new UserAuthorizeAttribute();
        private readonly int pageSize = 10;
        private static int pageCount;
        private static int choose;
        //
        // GET: /Users/

        public ActionResult Index(string id = default(string))
        {
            Users users = db_user.Search(id);
            
            if (users == null)
            {
                return HttpNotFound();
            }
            else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonStr = js.Serialize(photo.SearchTopNum(users.AlbumID, 4));
                ViewBag.photo = jsonStr;
            }
         //   Dispose();
            return View(users);
        }

        #region 登陆
        [HttpPost]
        public ActionResult LoginFoot(string ReturnUrl)
        {
            //string ReturnUrl = "/" + ReturnUrlController + "/" + ReturnUrlAction;
            Session["LoginFair"] = "false";
            string userName = Request["userID"];
            string userPassword = Request["userPassword"];
            Users loginUser = db_user.Search(userName);
            if (loginUser != null && Md5Hash(userPassword) == loginUser.Password)
            {
                Session["userName"] = loginUser.Username;
                Session["imageUrl"] = loginUser.Photo_Address;
                Session["userId"] = loginUser.UserID;
            }
            else
            {
                ModelState.AddModelError("", "提供的用户名或密码不正确。");
                Session["LoginFair"] = "true";
            }
            return Redirect(ReturnUrl);
        }
        public ActionResult LoginOff()//退出操作
        {
            Session["userName"] = null;
            Session["imageUrl"] = null;
            Session["LoginFair"] = null;
            Session["userId"] = null;
            return Content("fsdaf");
        }
        #endregion
        #region 列表
        //[CheckLogin]
        public ActionResult List()
        {
            List<Users> u = db_user.Search();
            pageCount = u.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            //if (choose != null && choose == 3)
            //    ViewBag.Reload = true;
            choose = 1;
            return PartialView(u);
        }

        /// <summary>
        ///列表删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteList()
        {
            //if (Session["DeleteStation"] != null)
            //{
            //    if ((bool)Session["DeleteStation"])
            //        Response.Write("<script>alert('删除成功！')</script>");
            //    else
            //        Response.Write("<script>alert('删除失败！')</script>");
            //}
            List<Users> us = db_user.Search();
            pageCount = us.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            choose = 2;
            return PartialView("List");
        }
        #endregion

        [HttpPost]
        public ActionResult LoginHead(string account, string password)
        {
            if (db_user.HasUser(account, Md5Hash(password)) == null)
            {
                HttpCookie _cookie = new HttpCookie("User");
                _cookie.Values.Add("UserName", account);
                //_cookie.Values.Add("Password", Md5Hash(login.password));
                _cookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(_cookie);
            }
            return PartialView("LoginHead");
        }

        #region 管理用户 增 删 改 细节
        //
        // GET: /Users/Create
        // 增加用户视图
        public ActionResult Create()
        {
            return PartialView();
        }
        // 增加用户
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            //if (ModelState.IsValid)
            //{  
            try
            {
                if (!string.IsNullOrWhiteSpace(users.UserID) && !string.IsNullOrWhiteSpace(users.Username))
                {
                    if (users.Admission_Data.Date.CompareTo(DateTime.Now.Date) <= 0 && users.Admission_Data > DateTime.MinValue)
                    {
                        users.Password = "123456"; //默认密码
                        users.Password = Md5Hash(users.Password);
                        users.Photo_Address = "/Images/Users_Head/empty.png";
                        users.Unique_ID = db_user.AddUnique_ID(users.Username, users.Native);
                        //users.AlbumID = int.Parse(users.Unique_ID);
                        if (db_user.Add(users) == false)
                            return PartialView(users);
                        //db.SaveChanges();
                        return RedirectToAction("List");
                    }
                    else
                    {
                        Response.Write("<script>alert('请添加准确入学时间！')</script>");
                        return PartialView(users);
                    }
                }
                else
                {
                    Response.Write("<script>alert('请准确添加必填信息！')</script>");
                    return PartialView(users);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('请准确添加信息！')</script>");
                return PartialView(users);
            }
            //}
        }

        //
        // GET: /Users/Delete/5
        // 用户删除视图
        public ActionResult Delete(string id = null)
        {
            Users users = db_user.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }
        //
        // POST: /Users/Delete/5
        // 删除用户
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (db_user.Delete(id))
                return RedirectToAction("List");
            else
            {
                Response.Write("<script>alert('删除失败！')</script>");
                Users users = db_user.Search(id);
                return PartialView(users);
            }
            //db.users.Remove(users);
            //db.SaveChanges();         
        }
        // 多用户删除
        [HttpPost]
        public ActionResult MultDelete(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)
            {
                string strids = collection.GetValue("checks").AttemptedValue;
                ids = strids.Split(',');
                string num = ""; //检查是否删除勾选的
                foreach (string getid in ids)
                {
                    if (!db_user.Delete(getid))
                        num = num + getid + " ";
                }
                if (num.Length > 0)
                    Response.Write("<script>alert('" + num + "删除失败！')</script>");
                else
                    Response.Write("<script>alert('删除成功！')</script>");
            }
            else
                Response.Write("<script>alert('无用户选中！')</script>");
            return RedirectToAction("DeleteList");
        }

        // 修改用户信息视图
        public ActionResult Manage_Edit(string id = null)
        {
            Users users = db_user.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }
        // 修改用户信息
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage_Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                if (db_user.Save(user))
                    Response.Write("<script>alert('保存成功！')</script>");
                else
                    Response.Write("<script>alert('保存失败！')</script>");
            }
            return PartialView(user);
        }

        //
        // GET: /Users/Details/5
        // 用户信息细节
        public ActionResult Details(string id = null)
        {
            Users users = db_user.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }
        #endregion

        #region 个人用户编辑信息
        //
        // GET: /Users/Edit/5

        public ActionResult Edit(string id = null)
        {
            Users users = db_user.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }
        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(users).State = EntityState.Modified;
                //db.SaveChanges();
                users.Password = Md5Hash(users.Password);
                db_user.Update(users);
                return RedirectToAction("Index", "Users", new {id=users.UserID });
            }
            return View(users);
        }
        #endregion


        /// <summary>
        /// 绘制验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            int _verificationLength = 6;
            int _width = 100, _height = 20;
            //SizeF _verificationTextSize;
            //Bitmap _bitmap = new Bitmap(Server.MapPath("~/Skins/Common/Texture.jpg"), true);
            //TextureBrush _brush = new TextureBrush(_bitmap);
            ////获取验证码
            //string _verificationText = Common.Text.VerificationText(_verificationLength);
            ////存储验证码
            //Session["VerificationCode"] = _verificationText.ToUpper();
            //Font _font = new Font("Arial", 14, FontStyle.Bold);
            //Bitmap _image = new Bitmap(_width, _height);
            //Graphics _g = Graphics.FromImage(_image);
            ////清空背景色
            //_g.Clear(Color.White);
            //绘制验证码
            //_verificationTextSize = _g.MeasureString(_verificationText, _font);
            //_g.DrawString(_verificationText, _font, _brush, (_width - _verificationTextSize.Width) / 2, (_height - _verificationTextSize.Height) / 2);
            //_image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return null;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }



        #region 改变头像  2016.12.4 zsl
        public ActionResult ChengePhoto(string id = null)
        {
            Users users = db_user.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            UploadImageModel up = new UploadImageModel();
            up.userID = users.UserID;
            up.address = users.Photo_Address;
            return View(up);
        }
        [HttpPost]
        public ActionResult PreviewImage()
        {
            var bytes = new byte[0];
            ViewBag.Mime = "image/png";

            if (Request.Files.Count == 1)
            {
                bytes = new byte[Request.Files[0].ContentLength];
                Request.Files[0].InputStream.Read(bytes, 0, bytes.Length);
                ViewBag.Mime = Request.Files[0].ContentType;
            }

            ViewBag.Message = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
            return PartialView();
        }

        [HttpPost]
        public ActionResult uploadHead(HttpPostedFileBase head)//命名和上传控件name 一样
        {
            try
            {
                if ((head == null))
                {
                    return Json(new { msg = 0 });
                }
                else
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png", "gif", "bmp" };
                    var fileExt = System.IO.Path.GetExtension(head.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        return Json(new { msg = -1 });
                    }

                    if (head.ContentLength > 1024 * 1000 * 10)
                    {
                        return Json(new { msg = -2 });
                    }

                    Random r = new Random();
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".";
                    var filename = FileName + fileExt;
                    var filepath = Path.Combine(Server.MapPath("~/Images/Users_Head/Users_FuBen"), filename);
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                    head.SaveAs(filepath);
                    return Json(new { msg = filename });
                }
            }
            catch (Exception)
            {
                return Json(new { msg = -3 });
            }
        }





        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveHead()
        {
            UploadImageModel model = new UploadImageModel();
            model.headFileName = Request.Form["headFileName"].ToString();
            model.x = Convert.ToInt32(Request.Form["x"]);
            model.y = Convert.ToInt32(Request.Form["y"]);
            model.width = Convert.ToInt32(Request.Form["width"]);
            model.height = Convert.ToInt32(Request.Form["height"]);
            model.userID = Request.Form["userID"].ToString();
            model.address = Request.Form["address"].ToString();
            if (model == null)
            {
                return Json(new { msg = 0 });
            }
            else
            {

                var filepath = Path.Combine(Server.MapPath("~/Images/Users_Head/Users_FuBen"), model.headFileName);
                string fileExt = Path.GetExtension(filepath);
                Random r = new Random();
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                var filename = FileName + fileExt;
                var path180 = Path.Combine(Server.MapPath("~/Images/Users_Head"), filename);
                string path = "/Images/Users_Head/" + filename;
                cutAvatar(filepath, model.x, model.y, model.width, model.height, 75L, path180, 180, model.userID, path);
                return Json(new { msg = 1 });
            }




        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        public void cutAvatar(string imgSrc, int x, int y, int width, int height, long Quality, string SavePath, int t, string userID, string path)
        {


            Image original = Image.FromFile(imgSrc);

            Bitmap img = new Bitmap(t, t, PixelFormat.Format24bppRgb);

            img.MakeTransparent(img.GetPixel(0, 0));
            img.SetResolution(72, 72);
            using (Graphics gr = Graphics.FromImage(img))
            {
                if (original.RawFormat.Equals(ImageFormat.Jpeg) || original.RawFormat.Equals(ImageFormat.Png) || original.RawFormat.Equals(ImageFormat.Bmp))
                {
                    gr.Clear(Color.Transparent);
                }
                if (original.RawFormat.Equals(ImageFormat.Gif))
                {
                    gr.Clear(Color.White);
                }


                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                using (var attribute = new System.Drawing.Imaging.ImageAttributes())
                {
                    attribute.SetWrapMode(WrapMode.TileFlipXY);
                    gr.DrawImage(original, new Rectangle(0, 0, t, t), x, y, width, height, GraphicsUnit.Pixel, attribute);
                }
            }
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
            if (original.RawFormat.Equals(ImageFormat.Jpeg))
            {
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
            }
            else
                if (original.RawFormat.Equals(ImageFormat.Png))
                {
                    myImageCodecInfo = GetEncoderInfo("image/png");
                }
                else
                    if (original.RawFormat.Equals(ImageFormat.Gif))
                    {
                        myImageCodecInfo = GetEncoderInfo("image/gif");
                    }
                    else
                        if (original.RawFormat.Equals(ImageFormat.Bmp))
                        {
                            myImageCodecInfo = GetEncoderInfo("image/bmp");
                        }

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            if (System.IO.File.Exists(SavePath))
            {
                System.IO.File.Delete(SavePath);
            }
            Users users = db.users.Find(userID);
            users.Photo_Address = path;
            db.Entry(users).State = EntityState.Modified;
            db.SaveChanges();
            img.Save(SavePath, myImageCodecInfo, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        #endregion


        /// <summary>
        /// 分页
        /// add by llt 2017.2.6
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxPaging(int pageIndex = 1)
        {
            List<Users> users = db_user.Search();
            List<Users> u = db_user.SearchPaging(users, pageIndex, pageSize);
            if (choose == 1)
                return PartialView("Table", u);
            else
                return PartialView("DeleteTable", u);
        }

        /// <summary>  
        /// 检查用户学号是否有重复  
        /// </summary>  
        /// <param name="userName">用户在页面(视图)表单中输入的UserName</param>  
        /// <returns>Json</returns>  
        public JsonResult CheckUserID(string UserID)
        {
            //bool result = true;
            bool result = db_user.HasUser(UserID);
            //if (user_aa.GetUser(userID) == null)
            //    result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void CreateAblum(string ID)
        {


        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        #region 柱状图 BLM
        /// <summary>
        /// 各年级人数
        /// </summary>
        /// <returns></returns>
        public ActionResult EachClass()
        {
            List<int> list = new List<int>();
            //本科生
            list.AddRange(db_user.BEachClass());
            ////研究生
            list.AddRange(db_user.YEachClass());
            return Json(list);
        }
        #endregion

    }
}