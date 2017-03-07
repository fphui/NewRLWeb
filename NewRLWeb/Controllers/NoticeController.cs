using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Models;
using NewRLWeb.Package;
using System.Globalization;
using NewRLWeb.ViewModels;
using NewRLWeb.ViewCode;
using System.Net.Mail;
using System.Text;
using NewRLWeb.Filters;
namespace NewRLWeb.Controllers
{
    public class NoticeController : Controller
    {
        private rlwzContext db = new rlwzContext();
        private Logic_Notice dbnotice = new Logic_Notice();
        private readonly int pageSize = 10; //每页显示数量
        private static int pageCount; //数据数量
        private static int? choose;


        public ActionResult Index(string type)
        {
            List<Notice> notice = dbnotice.Search(type);
            pageCount = notice.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Type=type;
            return View();
        }

        #region 列表
        [CheckLogin]
        public ActionResult List(int pageIndex = 1)
        {
            List<Notice> news = dbnotice.Search();
            pageCount = news.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Notice";
            //if (choose != null && choose == 3)
            //{
            //    ViewBag.Reload = true;
            //    ViewBag.Sider = 1;
            //}               
            choose = 1;
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView();
        }
        [CheckLogin]
        public ActionResult DeleteList(int pageIndex = 1)
        {
            List<Notice> notice = dbnotice.Search();
            pageCount = notice.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = pageCount;
            ViewBag.Title = "Notice";
            choose = 2;
            //return PartialView("~/Views/Manage/List.cshtml");
            return PartialView("List");
        }
        #endregion

        #region 增 删 改 细节
        //
        // GET: /Notice/Create
        // 公告增加视图
        [CheckLogin]
        public ActionResult Create()
        {
            return PartialView();
        }
        //
        // POST: /Notice/Create
        // 公告增加
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Notice notice, FormCollection collection)
        {
            notice.Publicationtime = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                try
                {
                    if (dbnotice.Add(notice))
                    {
                        if (SendMail(notice, collection))
                            Response.Write("<script>alert('添加成功，邮件发送完毕！')</script>");
                        else
                            Response.Write("<script>alert('添加成功，无邮件发送！')</script>");
                        //ViewBag.Reload = true;
                        //ViewBag.Sider = 1;
                        return RedirectToAction("List");
                    }
                    else
                    {
                        Response.Write("<script>alert('添加失败！')</script>");
                        return PartialView(notice);
                    }

                    //db.SaveChanges();             
                    //Response.Write("<script>alert('添加成功！')</script>");
                    //return RedirectToAction("../E_mail/Index");

                }
                catch
                {
                    Response.Write("<script>alert('添加失败！')</script>");
                    //return RedirectToAction("../E_mail/Index");
                }
            }
            return PartialView(notice);
        }

        //
        // GET: /Notice/Delete/5
        // 公告删除视图
        public ActionResult Delete(int id = 0)
        {
            //Notice notice = db.notice.Find(id);
            Notice notice = dbnotice.SearchByID(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return PartialView(notice);
        }
        //
        // POST: /Notice/Delete/5
        // 公告删除
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbnotice.Delete(id);
            return RedirectToAction("List");
        }
        // 公告多份删除
        [HttpPost]
        public ActionResult MultDelete(FormCollection collection)
        {
            string[] ids = { };
            if (collection.GetValues("checks") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL;
            {
                string strids = collection.GetValue("checks").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                ids = strids.Split(',');
                foreach (string getid in ids)
                {
                    int ID = Convert.ToInt32(getid);
                    dbnotice.Delete(ID);
                }
                Response.Write("<script>删除成功！</script>");//成功删除
            }
            return RedirectToAction("DeleteList");
        }

        //
        // GET: /Notice/Edit/5
        // 公告编辑视图
        public ActionResult Edit(int id = 0)
        {
            Notice notice = dbnotice.SearchByID(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            //notice.Publicationtime.ToString("yyyy年/MM月/dd日", DateTimeFormatInfo.InvariantInfo);
            return PartialView(notice);
        }
        //
        // POST: /Notice/Edit/5
        // 公告编辑
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Notice notice, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                if (dbnotice.Update(notice))
                {
                    if (SendMail(notice, collection))
                        Response.Write("<script>alert('修改成功，邮件发送完毕！')</script>");
                    else
                        Response.Write("<script>alert('修改成功，无邮件发送！')</script>");
                }
                else
                    Response.Write("<script>alert('修改失败！')</script>");
                //choose = 3;
                //return RedirectToAction("List");
            }
            return PartialView(notice);
        }

        //
        // GET: /Notice/Details/5
        // 公告细节
        public ActionResult Details(int id = 0)
        {
            Notice notice = dbnotice.SearchByID(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return PartialView(notice);
        }
        #endregion
        public ActionResult AjaxPagingIndex( string Type,int pageIndex = 1)
        {
            List<Notice> n = dbnotice.Search(Type); //数据库取数据
            List<Notice> notices = dbnotice.SearchPaging(n, pageIndex, pageSize);
            return PartialView("IndexTable", notices.ToList());
        }
        /// <summary>
        /// 分页
        /// add by llt 2017.1.17
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult AjaxPaging(int pageIndex = 1)
        {
            List<Notice> n = dbnotice.Search(); //数据库取数据
            List<Notice> notices = dbnotice.SearchPaging(n, pageIndex, pageSize);
            //for (int i = (pageIndex - 1) * pageSize; i < pageIndex * pageSize; i++)
            //{
            //    if (i == pageCount)
            //        break;
            //    notices.Add(n[i]);
            //}
            if (choose == 1)
                return PartialView("Table", notices.ToList());
            else
                return PartialView("DeleteTable", notices.ToList());

        }
        //public ActionResult DeleteAjaxPaging(int pageIndex = 1)
        //{
        //    List<Notice> n = dbnotice.Search(); //数据库取数据
        //    List<Notice> notices = new List<Notice>();
        //    for (int i = (pageIndex - 1) * pageSize; i < pageIndex * pageSize; i++)
        //    {
        //        if (i == pageCount)
        //            break;
        //        notices.Add(n[i]);
        //    }
        //    return PartialView("DeleteTable", notices.ToList());
        //}

        // 发送邮件页面
        public ActionResult E_mail()
        {
            E_mail eMail = new E_mail();
            List<EmailTo> email = eMail.emailTo();
            return PartialView(email);
        }
        /// <summary>
        /// 群发邮件
        /// add by llt 2017.2.4
        /// </summary>
        /// <param name="e"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool SendMail(Notice e, FormCollection collection)
        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            //bool s = true;
            string smtpServer = "smtp.qq.com"; //SMTP服务器
            string mailFrom = "1132981872@qq.com"; //登陆用户名
            string userPassword = "wrzzpyeliqkqgbjj";//登陆密码
            string[] lstRoles = { };
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码
            if (collection.GetValues("Mail") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL;
            {
                string strRoles = collection.GetValue("Mail").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                lstRoles = strRoles.Split(',');

                foreach (string r in lstRoles)
                {
                    // 发送邮件设置        
                    MailMessage mailMessage = new MailMessage(mailFrom, r); // 发送人和收件人
                    mailMessage.Subject = "日立项目组通知公告";//主题
                    mailMessage.Body = e.Coverage;//内容
                    mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                    mailMessage.IsBodyHtml = true;//设置为HTML格式
                    mailMessage.Priority = MailPriority.Low;//优先级
                    try
                    {
                        smtpClient.Send(mailMessage); // 发送邮件
                    }
                    catch (SmtpException ex)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false;
            //return RedirectToAction("../Notice/List");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}