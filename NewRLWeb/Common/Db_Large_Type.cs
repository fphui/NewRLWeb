using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Large_Type:BaseDb
    {
        #region 增 删
        public bool Add(Large_type ltype)
        {
            try
            {
                int query = (from o in context.large_type
                             where o.L_Type == ltype.L_Type
                             select o).Count();
                if (query == 0)
                    context.large_type.Add(ltype);
                return context.SaveChanges() > 0 ? true : false;
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
                var query = (from o in context.large_type
                             where o.ID == id
                             select o).SingleOrDefault();
                context.large_type.Remove(query);
                return context.SaveChanges() >= 1 ? true : false;
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
                var query = (from o in context.large_type
                             where o.L_Type == ltype
                             select o).SingleOrDefault();
                context.large_type.Remove(query);
                return context.SaveChanges() >= 1 ? true : false;
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
                var query = (from o in context.large_type
                             where o.ID == ltype.ID
                             select o).SingleOrDefault();
                query.L_Type = ltype.L_Type;
                query.Retain = ltype.Retain;
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查找
        /// <summary>
        /// 查找全部
        /// </summary>
        /// <returns></returns>
        public List<Large_type> Search() 
        {
            try
            {
                var questResult = (from o in context.large_type
                                   select o
                                   ).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据ID查找
        /// add by llt 2017.2.18
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Large_type SearchbyID(int id)
        {
            try
            {
                var questResult = (from o in context.large_type
                                   where o.ID == id
                                   select o
                                   ).SingleOrDefault();
                return questResult;
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
                var questResult = (from o in context.large_type
                                   where o.L_Type == ltype
                                   select o
                                   ).SingleOrDefault();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查找可以删除的大类别
        /// 编写人：夏禹
        /// </summary>
        /// <returns></returns>
        public List<Large_type> SearchCouldDelete()
        {
            try
            {
                var questResult = (from o in context.large_type
                                    where o.Retain==1 
                                   select o 
                                   ).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}