using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Notice : BaseDb
    {
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
                //判断是否有重复
                if (HasSameNotice(nt))
                {
                    return false;
                }
                else
                {
                    context.notice.Add(nt);
                    return context.SaveChanges() > 0 ? true : false;
                }
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
                var model = (from o in context.notice
                             where o.NoticeID == id
                             select o).Single();
                context.notice.Remove(model);
                return context.SaveChanges() >= 1 ? true : false;
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
                var query = (from o in context.notice
                             where o.NoticeID == nt.NoticeID
                             select o).Single();
                query.Author = nt.Author;
                query.Coverage = nt.Coverage;
                if (query.Publicationtime.Date.CompareTo(nt.Publicationtime.Date) != 0)
                    query.Publicationtime = nt.Publicationtime;
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据公告发布日期倒序查询全部
        /// </summary>
        /// <returns></returns>
        public List<Notice> Search()
        {
            try
            {
                var questResult = (from o in context.notice
                                   orderby o.Publicationtime descending
                                   select o 
                                   ).ToList();
                return questResult;
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
                if (!Type.Equals("全部"))
                {
                    var questResult = (from o in context.notice
                                       where o.Type.Equals(Type)
                                       orderby o.Publicationtime descending
                                       select o
                                   ).ToList();
                    return questResult;
                }
                else
                    return Search(); 
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
                var questResult = (from o in context.notice
                                   where o.NoticeID == id
                                   select o
                                   ).First();
                return questResult;
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
        ///change by llt 2017.1.16
        ///</summary>
        public List<Notice> SearchtheFirstN(int n)
        {
            try
            {
                //根据发布时间取最新的前n个
                var questResult = (from o in context.notice
                                   orderby o.Publicationtime descending
                                   select o).Take(n).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///获取最新一条数据---2017.1.14  zsl
        ///</summary>
        public Notice SearchOne()
        {
            try
            {
                var questResult = (from o in context.notice
                                   orderby o.Publicationtime descending
                                   select o).Take(1).First();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Notice> SearchFuzzy(string word)
        {
            try
            {
                var query = (from o in context.notice
                             where o.Coverage.Contains(word)
                             orderby o.Publicationtime descending
                             select o).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检查同内容公告
        ///周煜 2016 12 7
        ///检查是否再同一时间发布同一事件公告
        public bool HasSameNotice(Notice notice)
        {
            try
            {
                var query = (from o in context.notice
                            where o.Publicationtime == notice.Publicationtime && o.Coverage == notice.Coverage
                            select o);
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