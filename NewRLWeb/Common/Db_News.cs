using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.Common
{
    public class Db_News: BaseDb
    {
        //private rlwzContext db = new rlwzContext();
         #region 增 删 改
        /// <summary>
        /// 增加单跳数据
        /// add by llt 2017.2.13
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public bool Add(News news)
        {
            try
            {
                context.news.Add(news);
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
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
                var query = (from o in context.news
                             where o.NewsID == id
                             select o).SingleOrDefault();
                context.news.Remove(query);
                return context.SaveChanges() >= 1 ? true : false;
            }
            catch(Exception ex)
            {
                return false;
                throw ex;
                
            }
        }
        
        /// <summary>
        /// 新闻更新
        /// add by llt 2017.1.19
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public bool Update(News news)
        {
            try
            {
                //db.Entry(news);
                var query = (from o in context.news
                             where o.NewsID == news.NewsID
                             select o).Single();
                query.Abstract = news.Abstract;
                query.Author = news.Author;
                query.Coverage = news.Coverage;
                query.CoverageTxt = news.CoverageTxt;
                query.Glance_Number = news.Glance_Number;
                query.Link = news.Link;
                query.News_Type = news.News_Type;
                query.NewsSource = news.NewsSource;
                query.PhotoAddress = news.PhotoAddress;
                if (query.Publicationtime.Date.CompareTo(news.Publicationtime.Date) != 0)
                    query.Publicationtime = news.Publicationtime;
                query.Title = news.Title;
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion 
        
        #region 查询
        /// <summary>
        /// 根据新闻发布时间倒序查询全部
        /// </summary>
        /// <returns></returns>
        public List<News> Search()
        {
            try
            {
                var queryResult = (from o in context.news
                                   orderby o.Publicationtime descending
                                   select o
                            ).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据新闻发布时间倒序查询全部
        /// </summary>
        public List<News> SearchByTime(string type)
        {            
            try
            {
                var queryResult = (from o in context.news
                                   orderby o.Publicationtime descending
                                   select o
                            ).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <returns></returns>
        public List<News> SearchByType(string type)
        {
            try
            {
                var query = (from o in context.news
                             where o.News_Type == type
                             orderby o.Publicationtime descending
                             select o).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询新闻ID
        /// add by llt 2017.1.14
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public News SearchbyID(int id)
        {
            try
            {
                var query = (from o in context.news
                             where o.NewsID == id
                             select o).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询新闻通过标题
        /// add by llt 2017.2.1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public News SearchbyTitle(string name)
        {
            try
            {
                var query = (from o in context.news
                             where o.Title == name
                             select o).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取前n条记录 ---2017.1.17 zsl
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<News>SearchFirstN(int n)
        {
            try
            {
                var query = (from o in context.news
                             orderby o.Publicationtime descending
                             select o).Take(n).ToList();
                return query;
            }
            catch(Exception ex)
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
                var query = (from o in context.news
                             select o.NewsID).Max();
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
                var query = (from o in context.news
                             where o.NewsID==id
                             select o.Coverage).First();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<News> SearchFuzzy(string word)
        {
            try
            {
                var query = (from o in context.news
                             where o.Title.Contains(word) || o.Abstract.Contains(word)
                             orderby o.Publicationtime descending
                             select o).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region  判断新闻名 是否重复 
        // add by 周煜 2016.12.8
        public bool HasSameNews(string Bigtitle)
        {
            try
            {
                var query = from o in context.news
                            where o.Title == Bigtitle 
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
        ///// <summary>
        ///// 编辑新闻判断标题是否重复
        ///// add by llt 2017.2.3
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="Bigtitle"></param>
        ///// <returns></returns>
        //public bool HasSameNews(int id,string Bigtitle)
        //{
        //    try
        //    {
        //        if (context.news.Find(id).Title != Bigtitle)
        //        {
        //            return HasSameNews(Bigtitle);
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        #endregion

        ///// <summary>
        ///// 分页查询+特定个数
        ///// add by llt 2017.2.3
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="size"></param>
        ///// <returns></returns>
        //public List<News> SearchPaging(int page,int size)
        //{
        //    try
        //    {
        //        var queryResult = (from o in context.news
        //                           orderby o.Publicationtime descending
        //                           select o
        //                    ).Take(size*page).Skip(size*(page-1)).ToList();
        //        return queryResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}