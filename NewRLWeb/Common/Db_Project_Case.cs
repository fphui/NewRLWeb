using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Project_Case:BaseDb
    {
        #region 增 删 改
        public bool Add(Project_Case project)
        {
            try
            {
                context.project_case.Add(project);
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除单条数据
        /// add by llt 2017.1.15
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                var query = (from o in context.project_case
                             where o.ProjectID == id
                             select o).SingleOrDefault();
                context.project_case.Remove(query);
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(Project_Case project)
        {
            try
            {
                var model = (from o in context.project_case
                             where o.ProjectID == project.ProjectID
                             select o).SingleOrDefault();
                //context.Entry(ae).State = EntityState.Modified;
                model.Abstract = project.Abstract;
                model.Projectname = project.Projectname;
                model.Startdate = project.Startdate;
                model.Coverage = project.Coverage;
                model.Enddate = project.Enddate;
                model.ProjectType = project.ProjectType;
                model.PhotoAddress = project.PhotoAddress;
                model.Cooperativeunit = project.Cooperativeunit;
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
        /// 根据项目结束日期倒序查找全部
        /// </summary>
        /// <returns></returns>
        public List<Project_Case> Search()
        {
            try
            {
                var questResult = (from o in context.project_case
                                   orderby o.Enddate descending
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
        /// 查询项目ID
        /// add by llt 2017.1.14
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project_Case SearchbyID(int id)
        {
            try
            {
                var query = (from o in context.project_case
                             where o.ProjectID == id
                             select o).SingleOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据项目类型查找全部
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<Project_Case> SearchByType(string Type)
        {
            try
            {
                if (Type != null)
                {
                    var queryResult = (from o in context.project_case
                                       where o.ProjectType == Type
                                       select o
                                ).ToList();
                    return queryResult;
                }
                else
                {
                    var queryResult = (from o in context.project_case
                                       select o
                                ).ToList();
                    return queryResult;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询通过名字
        /// add by llt 2017.2.12
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project_Case SearchbyName(string name)
        {
            try
            {
                var query = (from o in context.project_case
                             where o.Projectname == name
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
                var query = (from o in context.project_case
                             select o.ProjectID).Max();
                return query;
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
                var query = (from o in context.project_case
                             where o.ProjectID == id
                             select o.Coverage).First();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检查同名
        /// <summary>
        /// 检查是否有同名项目
        /// </summary>
        /// <param name="Bigtitle"></param>
        /// <returns></returns>
        public bool HasSameProject(string Bigtitle)
        {
            try
            {
                var query = from o in context.project_case
                            where o.Projectname == Bigtitle
                            select o;
                if (query == null || !query.Any())
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }
}