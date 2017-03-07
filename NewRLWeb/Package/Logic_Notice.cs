using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Notice
    {
        private Db_Notice dbnotice = new Db_Notice();

        #region 增 删 改
        ///<summary>
        ///2016.12.8
        ///卜令梅
        ///增加一条数据
        ///</summary>
        public bool Add(Notice nt)
        {
            try
            {
                return dbnotice.Add(nt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///2016.12.8
        ///卜令梅
        ///删除一条数据
        ///</summary>
        public bool Delete(int id)
        {
            try
            {
                return dbnotice.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///2016.12.8
        ///卜令梅
        ///更新单条数据
        ///</summary>
        public bool Update(Notice nt)
        {
            try
            {
                return dbnotice.Update(nt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询
        public List<Notice> Search()
        {
            try
            {
                return dbnotice.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Notice> Search(string Type)
        {
            try
            {
                return dbnotice.Search(Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///2016.12.8
        ///卜令梅
        ///根据ID查询单个公告数据
        ///</summary>
        public Notice SearchByID(int id)
        {
            try
            {
                return dbnotice.SearchByID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///2016.12.8
        ///卜令梅
        ///提取前n个
        ///</summary>
        public List<Notice> SearchtheFirstN(int n)
        {
            try
            {
                return dbnotice.SearchtheFirstN(n);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取最新一条数据---2016.1.14  zsl
        /// </summary>
        /// <returns></returns>
        public Notice SearchOne()
        {
            try
            {
                return dbnotice.SearchOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分页查询/特定个数
        /// </summary>
        /// <param name="no"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<Notice> SearchPaging(List<Notice> no, int page, int size)
        {
            try
            {
                if (page == 1 && size >= no.Count())
                {
                    return no;
                }
                return no.Take(size * page).Skip(size * (page - 1)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<Notice> SearchFuzzy(string word)
        {
            try
            {
                return dbnotice.SearchFuzzy(word);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region 检查同内容公告
        ///// <summary>
        ///// 发布时检查是否再同一时间发布同一事件公告
        ///// add by llt 2017.2.14
        ///// </summary>
        ///// <param name="notice"></param>
        ///// <returns></returns>
        //public bool HasSameFromCreate(Notice notice)
        //{
        //    try
        //    {
        //        return dbnotice.HasSameNotice(notice);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //#endregion
    }
}