using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;
using NewRLWeb.ViewCode;
using NewRLWeb.Package;
using NewRLWeb.Helpers;
using System;
using System.Web;
using System.IO;
using System.Linq;
//using PagedList;
//using PagedList.Mvc;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using NewRLWeb.Filters;
namespace NewRLWeb.Controllers
{
    public class GraduateController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private ViewCode.Graduate gra = new ViewCode.Graduate();
        private Logic_Users users = new Logic_Users();
        private Logic_Employment_information db_EI = new Logic_Employment_information();
        private ViewCode.Map map = new ViewCode.Map();
        private ViewCode.GraduateCount graduCount=new ViewCode.GraduateCount();
        private readonly int pageSize = 10;  //分页
        private static int pageCount;
        private static int choose;

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


        private static List<Users> gra_data = new List<Users>();
        public ActionResult Index()
       {
            List<System.DateTime?> years = users.SearchYear();  //获取有毕业的年份
            List<SelectListItem> listItem_year = new List<SelectListItem>();  //年份下拉框的绑定
            foreach (System.DateTime y in years)
            {
                listItem_year.Add(new SelectListItem { Text = y.Year.ToString(), Value = y.Year.ToString() });
            }
            ViewData["List1"] = new SelectList(listItem_year, "Value", "Text");
            List<SelectListItem> listItem_address = new List<SelectListItem>();  //地址下拉框的绑定
            foreach (string ad in Province.Provinces)
            {
                listItem_address.Add(new SelectListItem { Text = ad, Value = ad });
            }
            ViewData["List2"] = new SelectList(listItem_address, "Value", "Text");

            List<ViewModels.GraduateCount> g = graduCount.Search();
            return View(g);
        }

      
        public ActionResult Form()
        {
            return PartialView();
        }
        //邢亚男 从数据库取数据  2017/02/24
       public ActionResult Graduate_Info(int? time)
        {
            List<Users> gratime;
           if(time!=null)
           {
               gratime = users.SearchByTypetime(time);
           }
           else
           {
               gratime = users.Search();
           }
            pageCount = gratime.Count();
            ViewBag.Type = time;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            return PartialView(gratime);
        }

        public ActionResult Person(string ui)
        {
            ViewModels.Graduate graduate = new ViewModels.Graduate();
            graduate = gra.Search(ui);
            return View(graduate);
        }


        

        //
        //GET: /Graduate/
        public PartialViewResult  Map()
        {
            List<ViewModels.Map> m = map.GraduOfProvincelist();
            return PartialView(m);
        }

      //  private readonly int pageSize = 2;
        public ActionResult AjaxFenYe()
        {   
            System.DateTime ?firstyear = users.SearchYear().First();
            List<Users> graduate = users.Search(firstyear.ToString(), null, null, null);
            int count = graduate.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = count;// db.users.Count();
            //TempData["da"] = graduate;
            gra_data = graduate;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AjaxFenYe(string year = null, string education = null, string address = null, string name = null)
        {
            List<Users> graduate = users.Search(year, education, address, name);
            int count = graduate.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = count;// db.users.Count();
            //TempData["da"] = graduate;
            gra_data = graduate;
            return PartialView();
        }
       
        public List<Users> GetPerson(int pageIndex, int pageSize, ref int totalCount, List<Users> graduate)
        {
            var persons = graduate.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = graduate.Count();
            return persons.ToList();
        }
        //毕业生管理界面

        #region 管理毕业生列表
        /// <summary>
        /// 毕业生列表
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult List(int pageIndex = 1)
        {
            List<Users> gra = users.SearchGraduates(); 
            pageCount = gra.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Graduate";
            if (choose == 4)
                ViewBag.Reload = true;
            choose = 1;
            //if (Request.IsAjaxRequest()) 
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView();
        }
        /// <summary>
        /// 毕业生增加列表
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult Create()
        {
            List<Users> gra = (List<Users>)users.SearchShouldGra();
            pageCount = gra.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            choose = 2;
            return PartialView (gra);
        }
        /// <summary>
        /// 删除列表
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult DeleteList()
        {
            List<Users> gra = users.Search();
            pageCount = gra.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Graduate";
            choose = 3;
            //if (Request.IsAjaxRequest()) 
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView();
        }

        // POST: /Album/Delete/5
        // 确定删除
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComfirmed(string id)
        {
            Users user = users.Find(id);
            if (users.Delete(id))
                Response.Write("<script>alert('删除成功！')</script>");
            else
                Response.Write("<script>alert('删除失败！')</script>");
            List<Users> gra = users.SearchGraduates();
            pageCount = gra.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            choose = 1;
            return RedirectToAction("List");
        }
        /// <summary>
        /// 毕业去向信息列表
        /// </summary>
        /// <param name="unique_id"></param>
        /// <returns></returns>
        public ActionResult EiList(string unique_id)
        {
            List<Employment_Information> grainf = db_EI.Search(unique_id);
            return PartialView(grainf);
        }

        
        #endregion

        #region 管理毕业生 增 删 改 细节
        /// <summary>
        /// 毕业生多人添加
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultCreate(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)
            {
                string strids = collection.GetValue("checks").AttemptedValue;
                ids = strids.Split(',');
                string num = ""; //检查是否增加完全勾选的
                foreach (string getid in ids)
                {
                    if(!users.Update(getid,DateTime.Now.Date))
                        num = num + getid + " ";
                }
                //if (num.Length > 0)
                //    Response.Write("<script>alert('" + num + "增加失败！')</script>");
                //else
                //    Response.Write("<script>alert('增加成功！')</script>");            
            }
            else
                Response.Write("<script>alert('无毕业生选中！')</script>");
            List<Users> gra = (List<Users>)users.SearchShouldGra();
            pageCount = gra.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            choose = 2;
            return RedirectToAction("List");
        }
        //
        // POST: /Graduate/Create
        // 毕业流向信息添加视图
        public ActionResult CreateEi(string id)
        {
            Users gra = users.Search(id);
            Employment_Information info = new Employment_Information();
            ///////////////////////////////////
            if (db_EI.Search(int.Parse(id)) != null)
            {
                info = db_EI.Search(int.Parse(id));
                return PartialView(info);
            }
            //////////////////////////////
            //info.Unique_ID = gra.Unique_ID;
            info.UserID = gra.UserID;
            info.Unique_ID = gra.UserID;
            /////////
            return PartialView(info);
        }
        /// <summary>
        /// 毕业流向信息添加
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEi(Employment_Information ei)
        {
            ei.Add_Data = DateTime.Now;
            if (ModelState.IsValid)
            {
                //db.employment_information.Add(employment_information);
                //db.SaveChanges();

                if (db_EI.Search(int.Parse(ei.UserID)) != null)
                {
                    if (db_EI.Update(ei))
                    {
                        Users user = users.Search(ei.UserID);
                        user.Graduate_Direction = ei.Graduate_information;
                        users.Update(user);
                        return PartialView("Edit", user);
                    }
                }
                if (db_EI.Add(ei))
                {
                    Users user = users.Search(ei.UserID);
                    return PartialView("Edit", user);
                }
                //return RedirectToAction("Details/"+ei.UserID);             
            }
            return PartialView(ei);
        }

        //
        // GET: /Manage/Delete/5
        // 删除毕业生
        /// <summary>
        /// 周煜 2017.2.17
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id = null)
        {
            Users user = users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        /// <summary>
        /// 毕业生列表删除
        /// 周煜 2017.2.27
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultDelete(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)
            {
                string strids = collection.GetValue("checks").AttemptedValue;
                ids = strids.Split(',');
                string num = ""; //检查是否删除完全勾选的
                foreach (string getid in ids)
                {
                    Users user = users.Find(getid);
                    user.Graduate_Data = null;
                    //if (!users.Update(getid,null))
                    if (!users.Delete(getid))
                        num = num + getid + " ";
                }
                if (num.Length > 0)
                    Response.Write("<script>alert('" + num + "删除失败！')</script>");
                else
                    Response.Write("<script>alert('删除成功！')</script>");
            }
            else
                Response.Write("<script>alert('无毕业生选中！')</script>");
            List<Users> gra = users.SearchGraduates();
            pageCount = gra.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            choose = 3;
            return RedirectToAction("List");
        }

        /// <summary>
        /// 毕业流向信息删除
        /// add by llt 2017.2.8
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteEi(int id)
        {
            Employment_Information info = db_EI.Search(id);
            string unique_id = info.Unique_ID;
            if(db_EI.Delete(id))
                Response.Write("<script>alert('删除成功！')</script>");
            else
                Response.Write("<script>alert('删除失败！')</script>");
            List<Employment_Information> grainf = db_EI.Search(unique_id);
            return PartialView("EiList",grainf);
        }

        // 编写人：夏禹
        // GET: /Manage/Edit/5
        // 毕业生信息编辑视图
        [HttpGet]
        public ActionResult Edit(string id = null)
        {
            Users user = users.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            return PartialView(user);
        }

        //提交用户信息
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(users).State = EntityState.Modified;
                //user.Password = Md5Hash(user.Password);
                //if (id != null)
                //    user.Photo_Address = "/Images/Users_Head/" + id;
                // TempData["user"] = user;
                if(users.Save(user))
                    Response.Write("<script>alert('修改成功！')</script>");
                else
                    Response.Write("<script>alert('修改失败！')</script>");
                //   return RedirectToAction("List");
            }
            return PartialView(user);
        }
        [HttpPost]
        public ActionResult EditEi(int id, string info)
        {
            Employment_Information ei = db_EI.Search(id);
            if(!string.IsNullOrWhiteSpace(info))
            {
                
                if(ei.Graduate_information == info)
                    Response.Write("<script>alert('修改信息相同！')</script>");
                else
                {
                    ei.Graduate_information = info;
                    if(db_EI.Update(ei))                    
                        Response.Write("<script>alert('修改成功！')</script>");
                 
                    else
                        Response.Write("<script>alert('修改失败！')</script>");
                }
            }
            else
                Response.Write("<script>alert('修改失败！')</script>");
            List<Employment_Information> grainf = db_EI.Search(ei.Unique_ID);
            return PartialView("EiList", grainf);
            //return Json(false, JsonRequestBehavior.AllowGet);
        }
        
        // 毕业生细节
        public ActionResult Details(string id = null)
        {
            //Users users = db.users.Find(id);
            NewRLWeb.ViewModels.Graduate user = gra.Search(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }
        #endregion


        //提交头像文件
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit2(IEnumerable<HttpPostedFileBase> files)
        {
            //  Users user = u;
            //  string a = (string);
            if (files != null)
            {
                string realPath = "/Images/Users_Head/";
                string pathForSaving = Server.MapPath(realPath);
                TempData["user"] = users.Change(files, pathForSaving);
            }
            //   Response.Write("<script>alert('" + user.Photo_Address + "')</script>");

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
            if (choose == 1)
            {
                List<Users> gra = users.SearchGraduates();
                return PartialView("Table", users.SearchPaging(gra, pageIndex, pageSize));
            }
            else if (choose == 2)
            {
                List<Users> gra = users.SearchShouldGra();
                return PartialView("Create", users.SearchPaging(gra, pageIndex, pageSize));
            }
            else
            {
                List<Users> gra = users.SearchGraduates();
                return PartialView("DeleteTable", users.SearchPaging(gra, pageIndex, pageSize));
            }
        }
        /*邢亚男  2017/02/23  Graduate分页数据库读取数据*/
        public ActionResult AjaxPagingIndex(int? time,int pageIndex = 1)
        {
            List<Users> n;
            if (time != null)
            {
                n = users.SearchByTypetime(time);
            }
            else
            {
                n = users.Search();
            }
            List<Users> gra = users.SearchPaging(n, pageIndex, pageSize);
            return PartialView("IndexTable", gra.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}