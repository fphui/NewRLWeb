using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
using System.Data;
using System.Data.Entity;
namespace NewRLWeb.Common
{
    public class Db_Album : BaseDb
    {
        public List<Album> Search()
        {
            try
            {
                var questResult = (from o in context.album
                                   orderby o.Publicationtime descending
                                   select o).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Album> Search(String Type)
        {
            try
            {
                var questResult = (from o in context.album
                                   where o.Album_Type==Type
                                   orderby o.Publicationtime descending
                                   select o).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询前几条
        /// add by llt 2017.1.18
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<Album> SearchTopNum(int n)
        {
            try
            {
                var questResult = (from o in context.album
                                   where o.Album_Type != "个人相册"
                                   orderby o.AlbumID descending 
                                   select o).Take(n).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据名字查找相册
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Album SearchFromName(String name)
        {
            try
            {
                var questResult = (from o in context.album
                                   where o.Albumname == name
                                   select o).Single();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据名字查找id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SearchFromNameToID(String name)
        {
            try
            {
                var questResult = (from o in context.album
                                   where o.Albumname == name
                                   select o.AlbumID).First();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据相册ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Album SearchFromAlbumID(int id)
        {
            try
            {
                var questResult = (from o in context.album
                                   where o.AlbumID == id
                                   select o).Single();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 取出所有非个人相册集
        /// </summary>
        /// <returns></returns>
        public List<Album> SearchToAlbum()
        {
            try
            {
                var Result = (from o in context.album
                              where o.Album_Type != "个人相册"
                              select o).ToList();
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 相册增加
        /// </summary>
        /// <param name="album"></param>
        /// 
        /// <returns></returns>
        public bool CreateAlbum(Album album) {
            try
            {
                context.album.Add(album);
                if (context.SaveChanges() > 0)
                {
                    if (album.Link == null)
                    {
                        var query = (from o in context.album
                                     where o.Albumname == album.Albumname
                                     select o).Single();
                        query.Link = "/Album/Photos?id="+query.AlbumID+"&name="+query.Albumname;
                        if (context.SaveChanges() > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  删除相册
        ///  参数为相册id 
        ///  成功返回true 失败返回false
        /// 编写人：夏禹
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        ///相册删除
        public bool DeleteAlbum(int id)
        {
            try
            {
                var str = (from a in context.album
                           where a.AlbumID == id
                           select a).ToList();
                if (str.Count != 0)
                {
                    context.album.Remove(str.First());
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        // 存储相册的更改信息
        // 编写人：夏禹     
        public bool ChangeAlbumImfo(Album album)
        {
            try
            {
                if (!CheckSameName(album.Albumname, album.AlbumID))
                {
                    context.Entry(album).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        /// 检查相册是否重名
        /// CheckSameName(string AlbumName) 创建等时候使用
        /// CheckSameName(string AlbumName , int AlbumID）用于修改信息时使用
        /// 存在该名字则返回true，不存在则返回false
        /// 编写人：夏禹
        /// </summary>
        /// <returns></returns>
        public bool CheckSameName(string AlbumName)
        {
            var str = (from a in context.album
                       where (a.Albumname == AlbumName)
                       select a).ToList();

            if (str.Count > 0)
            {
                return true;
            }
            else
                return false;

        }
        public bool CheckSameName(string AlbumName, int AlbumID)
        {
            var str = (from a in context.album
                       where (a.Albumname == AlbumName && a.AlbumID != AlbumID)
                       select a).ToList();

            if (str.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        #region  周煜  2016.12.7
        ///<查询相册是否存在>
        public bool HasAlbumName(string albumName)
        {
            try
            {
                var query = from o in context.album
                            where o.Albumname == albumName
                            select o;
                if (query == null || !query.Any())
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 编辑相册判断相册名重复
        /// add by llt 2017.2.6
        /// </summary>
        /// <param name="id"></param>
        /// <param name="albumName"></param>
        /// <returns></returns>
        public bool HasAlbumName(int id,string albumName)
        {
            try
            {
                if (context.album.Find(id).Albumname != albumName)
                {
                    return HasAlbumName(albumName);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// 相册更新
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public bool Update(Album album)
        {
            try
            {
                //context.Entry(album).State = EntityState.Modified;
                var query = (from o in context.album
                             where o.AlbumID == album.AlbumID
                             select o).FirstOrDefault();
                query.Album_Type = album.Album_Type;
                query.Albumname = album.Albumname;
                //query.Folderpath = album.Folderpath;
                query.Link = album.Link;
                query.Pho_Address = album.Pho_Address;
                if (query.Publicationtime.Date.CompareTo(album.Publicationtime.Date) != 0)
                    query.Publicationtime = album.Publicationtime;
                if(context.SaveChanges() > 0)
                    return true;
                else
                    return false;
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}