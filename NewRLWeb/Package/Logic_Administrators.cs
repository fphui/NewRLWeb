using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
using NewRLWeb.Common;

namespace NewRLWeb.Package
{
    public class Logic_Administrators
    {
        private Db_Administrators admin = new Db_Administrators();
        //private Db_Users user = new Db_Users();
        public string Login(string Name,string Password)
        {
            string login = "";
            //Users u = user.HasUser(Name, Password);
            //if (u != null)
            //{
            //    login = u.Username + "," + "user";
            //    return login;
            //}
            Administrator a=admin.HasUser(Name,Password);
            if(a!=null)
            {
                //login = a.Username + "," + "admin";
                login = a.Username;
                return login;
            }
            return "";
        }
    }
}