using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Employment_information
    {
        private Db_Employment_Information dbEI = new Db_Employment_Information();

        #region 增 删 改
        public bool Add(Employment_Information model)
        {
            try
            {
                return dbEI.Add(model);
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
                return dbEI.Delete(id);
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
                return dbEI.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
      

        #region 查询
        /// <summary>
        /// 根据添加时间倒序查找毕业去向信息
        /// </summary>
        /// <param name="unique_id"></param>
        /// <returns></returns>
        public List<Employment_Information> Search(string unique_id)
        {
            try
            {
                return dbEI.Search(unique_id);
            }
            catch (Exception ex)
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
                return dbEI.Search(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

  
    }
}