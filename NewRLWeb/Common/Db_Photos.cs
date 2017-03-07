using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Photos:BaseDb
    {
        /// <summary>
        /// 根据图片ID查找图片
        /// </summary>
        /// <param name="photo_id"></param>
        /// <returns></returns>
        public List<Photos> Search(int photo_id)
        {
            try
            {
                var questResult = (from o in context.photos
                                   where o.PhotoID == photo_id
                                   select o).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据地址查询id
        /// </summary>
        /// <param name="photo_id"></param>
        /// <returns></returns>
        public  int  SearchFormAdress(string adress)
        {
            try
            {
                var questResult = (from o in context.photos
                                   where o.Address == adress
                                   select o.PhotoID).First();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 根据图片所属相册集查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Photos> SearchAlbumPhotos(int id)
        {
            try
            {
                var questResult = (from o in context.photos
                                   where o.PhotoID == id
                                   select o).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据大类别查找图片
        /// </summary>
        /// <param name="l_type"></param>
        /// <returns></returns>
        public List<Photos> Search(string l_type)
        {
            try
            {
                var questResult = (from o in context.photos
                                   where o.L_Type == l_type
                                   select o).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> Search(List<News> news,string Type)
        {
            try
            {
                List<string> questResult = new List<string>();
                foreach(var item in news)
                {
                    var quest = (from o in context.photos
                                 where o.AlbumID == item.NewsID && o.L_Type == Type
                                 select o.Address).SingleOrDefault();
                    questResult.Add(quest);
                }          
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更具id取出相册中的前几张
        /// </summary>
        /// <param name="id"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<string> SearchTopNum(int id, int n)
        {
            try
            {
                var quest = (from o in context.photos
                             where o.AlbumID == id
                             select o.Address).ToList();
                quest.Take(n).ToList();
                return quest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromAlbumID(int AlbumID)
        {
            try
            {
                var questResult = (from o in context.photos
                                   where o.AlbumID == AlbumID
                                   select o).ToList();
                foreach (var a in questResult)
                {
                    context.photos.Remove(a);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}