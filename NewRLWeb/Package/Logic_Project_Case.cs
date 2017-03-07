using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Project_Case
    {
        private Db_Project_Case dbPC = new Db_Project_Case();

        #region 增 删 改
        public bool Add(Project_Case project)
        {
            try
            {
                return dbPC.Add(project);
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
                return dbPC.Delete(id);
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
                return dbPC.Update(project);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public List<Project_Case> Search()
        {
            try
            {
                return dbPC.Search();
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
        /// 
        public Project_Case SearchbyID(int id)
        {
            try
            {
                return dbPC.SearchbyID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Project_Case> SearchByType(string type)
        {
            try
            {
                return dbPC.SearchByType(type);
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
                return dbPC.SearchbyName(name);
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
                return dbPC.SearchMaxID();
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
                return dbPC.SearchPath(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 分页查询+特定个数
        /// add by llt 2017.2.12
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// /*2017/02/22  邢亚男 Project分页SearchPaging()函数定义*/
        public List<Project_Case> SearchPaging(List<Project_Case> project, int page, int size)
        {
            try
            {
                if (page == 1 && size >= project.Count())
                {
                    return project;
                }
                return project.Take(size * page).Skip(size * (page - 1)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
       

        #region 检查同名
        public bool HasSameProject(string name)
        {
            try
            {
                return dbPC.HasSameProject(name);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 修改时检查是否有同名项目案例
        /// add llt by 2017.2.12
        /// </summary>
        /// <param name="ae"></param>
        /// <returns></returns>
        public bool HasSameProject(Project_Case project)
        {
            try
            {
                if (dbPC.SearchbyID(project.ProjectID).Projectname != project.Projectname)
                    return HasSameProject(project.Projectname);
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