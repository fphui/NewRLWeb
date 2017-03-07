using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewRLWeb.Package;
using NewRLWeb.Models;
using System.Text.RegularExpressions;
namespace NewRLWeb.Helpers
{
    public class UserAuthorizeAttribute //: AuthorizeAttribute
    {
        /// <summary>
        /// 核心【验证用户是否登陆】
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    //检查Cookies["User"]是否存在
        //    if (httpContext.Request.Cookies["User"] == null) return false;
        //    //验证用户名密码是否正确
        //    HttpCookie _cookie = httpContext.Request.Cookies["User"];
        //    string _userName = _cookie["UserName"];
        //    string _password = _cookie["Password"];
        //    httpContext.Response.Write("用户名:" + _userName);
        //    if (_userName == "" || _password == "") return false;
        //    Logic_Users _userRsy = new Logic_Users();
        //    if (_userRsy.HasUser(_userName, _password) == null) return true;
        //    else return false;
        //}

        public bool GetAuthorizeCore(HttpContextBase httpContext)
        {
            //检查Cookies["User"]是否存在
            if (httpContext.Request.Cookies["User"] == null) 
                return false;
            //验证用户名密码是否正确
            HttpCookie _cookie = httpContext.Request.Cookies["User"];
            string _userName = _cookie["UserName"];
            string _uniqueID = _cookie["UserID"];
            if (String.IsNullOrEmpty(_userName) && String.IsNullOrEmpty(_uniqueID)) 
                return false;
            Logic_Users _userRsy = new Logic_Users();
            if (_userRsy.HasUser(_userName, _uniqueID) == null) 
                return true;
            else 
                return false;
        }

        public Users GetUser(string account)
        {
            Logic_Users l_user=new Logic_Users();
            Users user =new Users(); //判断账号（是姓名、学号、还是邮箱）
            if (Regex.IsMatch(account, @"[\u4e00-\u9fa5]"))
                user = l_user.SearchbyName(account);
            else if (account.Contains('@'))
                user = l_user.SearchbyMail(account);
            else
                user = l_user.Search(account);
            return user;
        }
    }
}
