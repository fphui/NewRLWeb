using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.Package;
using NewRLWeb.Filters;
namespace NewRLWeb.Controllers
{
    public class TypesController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Logic_Large_type dbltype = new Logic_Large_type();
        private Logic_Small_Type dbstype = new Logic_Small_Type();
        //
        // GET: /Types/
        [CheckLogin]
        public ActionResult List()
        {
            List<Large_type> ltype = dbltype.Search();
            return PartialView(ltype);
        }

        //
        // GET: /Types/Details/5

        public ActionResult Details(string ltype = null)
        {
            //Large_type large_type = db.large_type.Find(id);
            //String ltype = id.L_Type;
            List<Small_Type> stypes = dbstype.Search(ltype);
            if (stypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.LType = ltype;
            return PartialView(stypes);
        }

        //
        // GET: /Types/Create
        [CheckLogin]
        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Types/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Large_type large_type)
        {
            if (ModelState.IsValid)
            {
                //db.large_type.Add(large_type);
                //db.SaveChanges();
                if (dbltype.Add(large_type))
                    Response.Write("<script>alert('添加成功')</script>");
                else
                    Response.Write("<script>alert('添加成功')</script>");
                return RedirectToAction("List");
            }

            return PartialView(large_type);
        }
        /// <summary>
        /// 添加小类别
        /// 修改人：夏禹
        /// </summary>
        /// <param name="small_type"></param>
        /// <returns></returns>
        public ActionResult AddSType(string ltype=null)
        {
            List<Small_Type> stypes = dbstype.Search(ltype);
            ViewBag.LType = ltype;
            //if (ltype != null)
            //{               
            //    ViewData["smallType"] = ltype;
            //}         
            return PartialView(stypes);
        }


        public ActionResult Add(Small_Type small_type)
        { 
            //small_type.L_Type =(string)TempData["smallType"];
            //small_type.Retain = 0;
            //if (ModelState.IsValid)
            //{
               
                if (dbstype.Add(small_type))
                    Response.Write("<script>alert('添加成功')</script>");
                else
                    Response.Write("<script>alert('添加失败')</script>");
                return RedirectToAction("List");
            //}
            
            //return PartialView(small_type);
        }
        //
        // GET: /Types/Edit/5

        public ActionResult Edit(int id)
        {
            //Large_type large_type = db.large_type.Find(id);
            Large_type large_type = dbltype.SearchbyID(id);
          
            if (large_type == null)
            {
                return HttpNotFound();
            }
            List<Small_Type> stypes = dbstype.Search(large_type.L_Type);
            ViewBag.ID = large_type.ID;
            ViewBag.Retain = large_type.Retain;
            ViewBag.LType = large_type.L_Type;
            return PartialView(stypes);
        }

        //
        // POST: /Types/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Large_type large_type)
        {
            //if (ModelState.IsValid)
            //{
            //db.Entry(large_type).State = EntityState.Modified;
            //db.SaveChanges();
            if (dbltype.Update(large_type))
                Response.Write("<script>alert('修改成功')</script>");
            else
                Response.Write("<script>alert('修改失败')</script>");
            //}
            List<Small_Type> stypes = dbstype.Search(large_type.L_Type);
            ViewBag.ID = large_type.ID;
            ViewBag.Retain = large_type.Retain;
            ViewBag.LType = large_type.L_Type;
            return PartialView(large_type);
        }

        //修改小类
        public ActionResult EditSt(int id, byte check)
        {
            Small_Type stype = dbstype.SearchbyID(id);
            dbstype.ChangeRetain(id, check);
            Large_type ltype = dbltype.SearchbyLType(stype.L_Type);
            List<Small_Type> stypes = dbstype.Search(ltype.L_Type);
            ViewBag.ID = ltype.ID;
            ViewBag.Retain = ltype.Retain;
            ViewBag.LType = ltype.L_Type;
            return PartialView("Edit", stypes);
        }
        //
        // GET: /Types/Delete/5

        public ActionResult Delete(string ltype = null)
        {
            List<Small_Type> stypes = dbstype.Search(ltype);
            if (stypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.LType = ltype;
            return PartialView(stypes);
        }

        //
        // POST: /Types/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string ltype)
        {
            //Large_type large_type = db.large_type.Find(id);
            //db.large_type.Remove(large_type);
            //db.SaveChanges();
            if(dbstype.Delete(ltype) && dbltype.Delete(ltype))
            {
                Response.Write("<script>alert('删除成功！')</script>");
                List<Large_type> largetype = dbltype.Search();
                return PartialView("List",largetype);
            }
            else
            {
                Response.Write("<script>alert('删除失败！')</script>");
                List<Small_Type> stypes = dbstype.Search(ltype);
                ViewBag.LType = ltype;
                return PartialView("Delete",stypes);
            }
        }
        [HttpPost]
        public ActionResult DeleteSt(int id)
        {
            Small_Type stype = dbstype.SearchbyID(id);

            if (dbstype.Delete(id))
                Response.Write("<script>alert('删除成功')</script>");
            else
                Response.Write("<script>alert('删除失败')</script>");
            Large_type ltype = dbltype.SearchbyLType(stype.L_Type);
            List<Small_Type> stypes = dbstype.Search(ltype.L_Type);
            ViewBag.ID = ltype.ID;
            ViewBag.Retain = ltype.Retain;
            ViewBag.LType = ltype.L_Type;
            return PartialView("Edit", stypes);
        }

        public ActionResult DeleteList()
        {
            List<Large_type> ltype = dbltype.SearchCouldDelete();
            return PartialView(ltype);
        }

        public ActionResult MultDelete(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)
            {
                string strids = collection.GetValue("checks").AttemptedValue;
                ids = strids.Split(',');
                foreach (string getid in ids)
                {
                   dbltype.Delete(Convert.ToInt32(getid));
                }
            }
            return RedirectToAction("DeleteList");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}