using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Academic_Exchange
    {
        private Db_Academic_Exchange dbAE = new Db_Academic_Exchange();

        #region 增 删 改
        public bool Add(Academic_Exchange ae)
        {
            try
            {
                return dbAE.Add(ae);
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
                return dbAE.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Academic_Exchange ae)
        {
            try
            {
                return dbAE.Update(ae);
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
        public List<Academic_Exchange> Search()
        {
            try
            {
                return dbAE.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Academic_Exchange SearchbyId(int id)
        {
            try
            {
                return dbAE.SearchbyId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //邢亚男  2017/02/27
        public List<Academic_Exchange> SearchByTypetime(int? time)
        {
            try
            {
                return dbAE.SearchByTypetime(time);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询通过名字
        /// add by llt 2017.2.11
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Academic_Exchange SearchbyName(string name)
        {
            try
            {
                return dbAE.SearchbyName(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public int SearchMaxID()
        {
            try
            {
                return dbAE.SearchMaxID();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <returns></returns>
        public string SearchPath(int id)
        {
            try
            {
                return dbAE.SearchPath(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分页查询+特定个数
        /// add by llt 2017.2.11
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<Academic_Exchange> SearchPaging(List<Academic_Exchange> ae, int page, int size)
        {
            try
            {
                if (page == 1 && size >= ae.Count())
                {
                    return ae;
                }
                return ae.Take(size * page).Skip(size * (page - 1)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检查同名
        ///周煜 检查是否有同名学术交流案例 2016 12 8
        ///
        public bool HasSameAcdemicExchange(string AcademicName)
        {
            try
            {
                return dbAE.HasSameAcdemicExchange(AcademicName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 修改时检查是否有同名学术交流案例
        /// add llt by 2017.2.12
        /// </summary>
        /// <param name="ae"></param>
        /// <returns></returns>
        public bool HasSameAcdemicExchange(Academic_Exchange ae)
        {
            try
            {
                if (dbAE.SearchbyId(ae.AcademicID).AcademicName != ae.AcademicName)
                    return HasSameAcdemicExchange(ae.AcademicName);
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}