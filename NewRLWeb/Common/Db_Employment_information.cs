using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Employment_Information:BaseDb
    {
        #region 增 删 改
        public bool Add(Employment_Information model)
        {
            try
            {
                context.employment_information.Add(model);
                return context.SaveChanges() >= 1 ? true : false;
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
                var query = (from o in context.employment_information
                             where o.ID == id
                             select o).SingleOrDefault();
                context.employment_information.Remove(query);
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Employment_Information model)
        {
            try
            {
                var query = (from o in context.employment_information
                             where o.ID == model.ID
                             select o).SingleOrDefault();
                query.Graduate_information = model.Graduate_information;
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查找
        /// <summary>
        /// 根据添加时间倒序查找毕业去向信息
        /// </summary>
        /// <param name="unique_id"></param>
        /// <returns></returns>
        public List<Employment_Information> Search(string unique_id) 
        {
            try
            {
                var queryResult = (from o in context.employment_information
                                   where o.Unique_ID == unique_id
                                   orderby o.Add_Data descending
                                   select o
                                  ).ToList();
                return queryResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据ID查找单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employment_Information Search(int id)
        {
            try
            {
                var queryResult = (from o in context.employment_information
                                   where o.ID == id
                                   select o
                                  ).SingleOrDefault();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}