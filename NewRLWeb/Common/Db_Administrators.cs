using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.Common
{
    public class Db_Administrators:BaseDb
    {
        /// <summary>
        /// 根据账号、密码 判断用户是否存
        /// 2017.1.15--zsl
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Administrator HasUser(string account, string password)
        {
            try
            {
                var queryResult = (from o in context.administrator
                                   where o.Username == account
                                   select o
                  ).ToList();
                //if (!String.IsNullOrEmpty(password))
                var q = queryResult.Where(p => p.Password == password).FirstOrDefault();
                return q;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}