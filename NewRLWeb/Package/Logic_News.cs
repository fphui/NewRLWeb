using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_News
    {
        private Db_News dbnews = new Db_News();

        #region 增 删 改
        /// <summary>
        /// 增加新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public bool Add(News news)
        {
            try
            {
                return dbnews.Add(news);
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
                return dbnews.Delete(id);
            }
            catch (Exception ex)
            {
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
                return dbnews.Update(news);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 查找
        /// <summary>
        /// 查找全部新闻
        /// </summary>
        /// <returns></returns>
        public List<News> Search()
        {
            try
            {
                return dbnews.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 排序按时间
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<News> SearchByTime(string type)
        {
            try
            {
                return dbnews.SearchByTime(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 按照type选择新闻
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<News> SearchByType(string type)
        {
            try
            {
                return dbnews.SearchByType(type);
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
                return dbnews.SearchbyID(id);
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
        /// <param name="name"></param>
        /// <returns></returns>
        public News SearchbyTitle(string name)
        {
            try
            {
                return dbnews.SearchbyTitle(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 获取前n条记录---2017.1.17zsl
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<News>SearchFirstN(int n)
        {
            try
            {
                return dbnews.SearchFirstN(n);
            }
            catch(Exception ex)
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
                return dbnews.SearchMaxID();
            }
            catch(Exception ex)
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
                return dbnews.SearchPath(id);
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
        public List<News> SearchFuzzy(string word)
        {
            try
            {
                return dbnews.SearchFuzzy(word);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 分页查询+特定个数
        /// add by llt 2017.2.3
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<News> SearchPaging(List<News> news, int page, int size)
        {
            try
            {
                if (page == 1 && size >= news.Count())
                {
                    return news;
                }
                return news.Take(size * page).Skip(size * (page - 1)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检查是否存在相同新闻 
        // add by 周煜 2016.12.8 
        public bool HasSameNews(string Bigtitle)
        {
            try
            {
                return dbnews.HasSameNews(Bigtitle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 编辑新闻时判断标题是否重复
        /// add by llt 2017.2.3
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Bigtitle"></param>
        /// <returns></returns>
        public bool HasSameNews(News news)
        {
            try
            {
                if (dbnews.SearchbyID(news.NewsID).Title != news.Title)
                {
                    return dbnews.HasSameNews(news.Title);
                }
                else
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