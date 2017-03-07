using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Album
    {
        private Db_Album dbalbum = new Db_Album();
        public List<Album> Search()
        {
            try
            {
                return dbalbum.Search();
                
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
                return dbalbum.Search(Type);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 分页查询+特定个数
        /// add by llt 2017.2.7
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<Album> SearchPaging(List<Album> albums, int page, int size)
        {
            try
            {
                if (page == 1 && size >= albums.Count())
                {
                    return albums;
                }
                return albums.Take(size * page).Skip(size * (page - 1)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Album> SearchTopNum(int n)
        {
            try
            {
                return dbalbum.SearchTopNum(n);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Album SearchFromName(String name)
        {
            try
            {
                return dbalbum.SearchFromName(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Album SearchFromAlbumID(int id)
        {
            try
            {
                return dbalbum.SearchFromAlbumID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try 
            {
                return dbalbum.DeleteAlbum(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public bool Create(Album album)
        {
            try
            {
                return dbalbum.CreateAlbum(album);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int SearchFromNameToID(String name)
        {
            try
            {            
                return dbalbum.SearchFromNameToID(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(Album album)
        {
            try
            {
                return dbalbum.Update(album);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ChangeAlbumImfo(Album album)
        {
            try 
            {
                return dbalbum.ChangeAlbumImfo(album);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 周煜2016.12.7
        ///查询该相册名是否已存在
        public bool HasAlbumName(string albumName)
        {
            try
            {
                return dbalbum.HasAlbumName(albumName);
            }
            catch (Exception e)
            {
                throw e;
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
                return dbalbum.HasAlbumName(id,albumName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}