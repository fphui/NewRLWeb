using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.Helpers;
namespace NewRLWeb.Controllers
{
    public class AdminController : Controller
    {
        private rlwzContext db = new rlwzContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.administrator.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(string id = null)
        {
            Administrator administrator = db.administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                //administrator.Password = MD5.Md5Hash(administrator.Password); //加密
                db.administrator.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(string id = null)
        {
            Administrator administrator = db.administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(string id = null)
        {
            Administrator administrator = db.administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Administrator administrator = db.administrator.Find(id);
            db.administrator.Remove(administrator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}