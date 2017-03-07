using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.ViewModels;
using NewRLWeb.ViewCode;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Util;

namespace NewRLWeb.Controllers
{
    /// <summary>
    /// 2017.1.14  zsl
    /// </summary>
    public class E_mailController : Controller
    {
        //
        // GET: /E_mail/
        private E_mail eMail = new E_mail();
        public ActionResult Index()
        {
            Email email = eMail.sendEmail();
            return View(email);
        }
        public ActionResult SendMail(Email e, FormCollection collection)
        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            bool s = true;
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
            }
            foreach (string r in lstRoles)
            {
                // 发送邮件设置        
                MailMessage mailMessage = new MailMessage(mailFrom, r); // 发送人和收件人
                mailMessage.Subject = e.mailSubject;//主题
                mailMessage.Body = e.mailContent;//内容
                mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                mailMessage.IsBodyHtml = true;//设置为HTML格式
                mailMessage.Priority = MailPriority.Low;//优先级
                try
                {
                    smtpClient.Send(mailMessage); // 发送邮件
                }
                catch (SmtpException ex)
                {
                    Response.Write("<script>alert('发送失败!');</script>");
                    //continue;
                }
            }
            return RedirectToAction("../Notice/List");
        }
    }
}
