using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
namespace NewRLWeb.Common
{
    public class Db_Academic_Exchange:BaseDb
    {
        #region 增 删 改
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="ae"></param>
        /// <returns></returns>
        public bool Add(Academic_Exchange ae)
        {
            try
            {
                context.academic_exchange.Add(ae);
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                var model = (from o in context.academic_exchange
                             where o.AcademicID == id
                             select o).SingleOrDefault();
                context.academic_exchange.Remove(model);
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="ae"></param>
        /// <returns></returns>
        public bool Update(Academic_Exchange ae)
        {
            try
            {
                var model = (from o in context.academic_exchange
                             where o.AcademicID == ae.AcademicID
                             select o).Single();
                //context.Entry(ae).State = EntityState.Modified;
                model.Abstract = ae.Abstract;
                model.AcademicName = ae.AcademicName;
                model.BeginTime = ae.BeginTime;
                model.Coverage = ae.Coverage;
                model.EndTime = ae.EndTime;
                model.Link = ae.Link;
                model.PhotoAddress = ae.PhotoAddress;
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
        /// 根据学术交流的结束日期查找全部
        /// </summary>
        /// <returns></returns>
        public List<Academic_Exchange> Search()
        {
            try
            {
                var questResult = (from o in context.academic_exchange
                                   orderby o.EndTime descending
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
        /// 查询单个学术交流数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Academic_Exchange SearchbyId(int id)
        {
            try
            {
                var questResult = (from o in context.academic_exchange
                                   where o.AcademicID==id
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
        /// 查询通过名字
        /// add by llt 2017.2.11
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Academic_Exchange SearchbyName(string name)
        {
            try
            {
                var query = (from o in context.academic_exchange
                             where o.AcademicName == name
                             select o).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取最大Id
        /// </summary>
        /// <returns></returns>
        public int SearchMaxID()
        {
            try
            {
                var query = (from o in context.academic_exchange
                             select o.AcademicID).Max();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //2017/02/27  邢亚男
        public List<Academic_Exchange> SearchByTypetime(int? data)
        {
            // Convert.ToString(
            try
            {

                //if (data != 0 && data != 1)//按年份取数据
                //{
                    var query = (from o in context.academic_exchange
                                 where o.EndTime.Year == data
                                       select o
                                  ).ToList();
                    return query;
                //}
                //else//按毕业生类别取数据
                //{
                //    var query = (from o in context.academic_exchange
                //                 where o.EndTime != null && o.Education == data
                //                       select o
                //                  ).ToList();
                //    return query;
                //}
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
                var query = (from o in context.academic_exchange
                             where o.AcademicID == id
                             select o.Coverage).First();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 查询是否有相同学术交流案例 
        // add by 周煜 2016 12 8
        public bool HasSameAcdemicExchange(string AcademicName)
        {
            try
            {
                var query = from o in context.academic_exchange
                            where o.AcademicName == AcademicName
                            select o;
                if (query == null || !query.Any())
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 

        
    }
}