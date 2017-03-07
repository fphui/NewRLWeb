using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;

namespace NewRLWeb.ViewCode
{
    public class E_mail
    {
        Email email = new Email();
        private Logic_Users user = new Logic_Users();
        private Logic_Notice notice = new Logic_Notice();


        /// <summary>
        /// 获取收件人姓名及邮箱
        /// </summary>
        /// <returns></returns>
        public List<EmailTo> emailTo()
        {
            List<Users> use = user.SearchNotGraduatesInfo();
            List<EmailTo> emailTo = new List<EmailTo>();            
            foreach(var item in use)
            {
                EmailTo e = new EmailTo();
                e.Name = item.Username;
                if(!string.IsNullOrEmpty(item.E_mail))
                {
                    e.Mail = item.E_mail;
                    emailTo.Add(e);
                }

            }
            return emailTo;
        }

        /// <summary>
        /// 发送邮件内容及收件人
        /// </summary>
        /// <returns></returns>
        public Email sendEmail()
        {
            List<EmailTo> email = emailTo();
            Email e = new Email();
            Notice no = notice.SearchOne();
            e.emailTo = email;
            e.mailContent = no.Coverage;
            e.mailSubject = "长春理工大学日立项目组公告";
            return e;
        }
    }
}