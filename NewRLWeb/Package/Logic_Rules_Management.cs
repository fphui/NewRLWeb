using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
namespace NewRLWeb.Package
{
    public class Logic_Rules_Management
    {
        private Db_Rules_Management dbRM = new Db_Rules_Management();
        public Rules_Management search()
        {
            try
            {
                return dbRM.search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //编写人夏禹
        ///2017.1.16
        //存储管理信息
        public string save(Rules_Management rules_management)
        {
            if (dbRM.save(rules_management))
                return "修改成功！";
            else
                return "修改失败！";
        }

        //编写人夏禹
        ///2017.1.16
        //更改管理文件
        public string save(Rules_Management rules_management, HttpPostedFileBase file, string pathForSaving)
        {
          
                        
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                        DateTime dt = DateTime.Now;
                        var filename = DateTime.Now.ToString("yyyyMMddHHmmss")+ "." + fileExt;
                        var path = Path.Combine(pathForSaving, filename);    
                        file.SaveAs(path);
                        rules_management.Coverage = filename;                               
                    }
            return save(rules_management);
        }
    }
}