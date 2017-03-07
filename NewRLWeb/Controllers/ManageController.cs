using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.Package;
using System.Security.Cryptography;
using System.Text;
using NewRLWeb.Filters;

namespace NewRLWeb.Controllers
{
    public class ManageController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Logic_Administrators Admin = new Logic_Administrators();
        //
        // GET: /Manage/
        [CheckLogin]
        public ActionResult Index()
        {
            return PartialView(db.users.ToList());
        }

        //
        // GET: /Manage/Details/5

        public ActionResult Details(string id = null)
        {
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // GET: /Manage/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        //
        // GET: /Manage/Edit/5

        public ActionResult Edit(string id = null)
        {
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Manage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        //
        // GET: /Manage/Delete/5

        public ActionResult Delete(string id = null)
        {
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Manage/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.users.Find(id);
            db.users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            Session["Name"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Administrator admin)
        {
            //string pass = "";
            //if (admin.Password != null && admin.Password != "")
            //    pass = Md5Hash(admin.Password);
            //string Login = Admin.Login(admin.Username, pass);
            string Login = Admin.Login(admin.Username, admin.Password);
            if (Login == "")
            {
                ModelState.AddModelError("", "提供的用户名或密码不正确。");
                return View(admin);
            }
            //string[] login = Login.Split(',');
            Session["Name"] = admin.Username;//login[0];
            //Session["Type"] = login[1];
            //if (login[1] == "admin")
            //{
                return RedirectToAction("Index");
            //}
            //else
            //{
            //    //return RedirectToAction("../Home/Index");
            //    return PartialView(admin);
            //}
        }

        ///// <summary>
        ///// 32位MD5加密
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //private static string Md5Hash(string input)
        //{
        //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        //    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        //    StringBuilder sBuilder = new StringBuilder();
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }
        //    return sBuilder.ToString();
        //}

        public ActionResult AlbumIndex()
        {

            return View(db.album.ToList());
        }

        public ActionResult Welcome()
        {
            return PartialView();
        }

        public ActionResult Content()
        {
            return View();
        }
    }
}