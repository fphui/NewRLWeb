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
using NewRLWeb.Filters;
namespace NewRLWeb.Controllers
{
    public class RulesController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Logic_Rules_Management logic = new Logic_Rules_Management();//zsl 2016.11.30
        //
        // GET: /Rules/

        public ActionResult Index()//zsl 2016.11.30
        {
            Rules_Management rm = new Rules_Management();
            rm = logic.search();
            return View(rm);
        }

        //
        // GET: /Rules/Details/5

        public ActionResult Details(int id = 0)
        {
            Rules_Management rules_management = db.rules_management.Find(id);
            if (rules_management == null)
            {
                return HttpNotFound();
            }
            return View(rules_management);
        }

        //
        // GET: /Rules/Create
        [CheckLogin]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rules/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rules_Management rules_management)
        {
            if (ModelState.IsValid)
            {
                db.rules_management.Add(rules_management);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rules_management);
        }


        //
        // GET: /Rules/Edit/5


        //修改人：夏禹 2017/1/16
        //案例管理修改
        public ActionResult Edit()
        {
            Rules_Management rules_management = db.rules_management.First();
            if (rules_management == null)
            {
                return HttpNotFound();
            }
            return PartialView(rules_management);
        }

        /// <summary>
        /// 2017.2.24       zsl
        /// </summary>
        /// <param name="rules_management"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rules_Management rules_management,HttpPostedFileBase files)
        {          
            Random r = new Random();
            string filePath = "~/Html/Rules/";
            var realpath = Server.MapPath(filePath);
            Logic_Rules_Management L = new Logic_Rules_Management();
            Response.Write("<script>alert('" + L.save(rules_management, files, realpath) + "')</script>");
            return View("Edit",rules_management);
        }
        //
        // GET: /Rules/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Rules_Management rules_management = db.rules_management.Find(id);
            if (rules_management == null)
            {
                return HttpNotFound();
            }
            return View(rules_management);
        }

        //
        // POST: /Rules/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rules_Management rules_management = db.rules_management.Find(id);
            db.rules_management.Remove(rules_management);
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