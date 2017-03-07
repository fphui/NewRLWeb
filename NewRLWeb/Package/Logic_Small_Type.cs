using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Small_Type
    {
        private Db_Small_Type dbST = new Db_Small_Type();
        public List<Small_Type> Search(string l_type)
        {
            try
            {
                return dbST.Search(l_type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取除个人以外小类别名称
        /// </summary>
        /// <param name="l_type"></param>
        /// <returns></returns>
        public List<Small_Type> SearchNewsType(string l_type)
        {
            try
            {
                return dbST.SearchNewsType(l_type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Small_Type SearchbyID(int id)
        {
            try
            {
                return dbST.SearchbyID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add(Small_Type s_type)
        {
            try
            {
                return dbST.Add(s_type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string ltype)
        {
            try
            {
                return dbST.Delete(ltype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return dbST.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangeRetain(int id,byte retain)
        {
            try
            {
                return dbST.ChangeRetain(id,retain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}