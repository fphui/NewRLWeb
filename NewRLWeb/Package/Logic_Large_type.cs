using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Large_type
    {
        private Db_Large_Type dbLT = new Db_Large_Type();

        #region 查找
        /// <summary>
        /// 查找全部
        /// </summary>
        /// <returns></returns>
        public List<Large_type> Search()
        {
            try
            {
                return dbLT.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查找根据ID
        /// </summary>
        /// <returns></returns>
        public Large_type SearchbyID(int id)
        {
            try
            {
                return dbLT.SearchbyID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Large_type SearchbyLType(string ltype)
        {
            try
            {
                return dbLT.SearchbyLType(ltype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查找可以删除的
        /// </summary>
        /// <returns></returns>
        public List<Large_type> SearchCouldDelete()
        {
            try
            {
                return dbLT.SearchCouldDelete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 增 删
        public bool Add(Large_type ltype)
        {
            try
            {
                return dbLT.Add(ltype);
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
                return dbLT.Delete(id);
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
                return dbLT.Delete(ltype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Large_type ltype)
        {
            try
            {
                return dbLT.Update(ltype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}