using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Photos
    {
        private Db_Photos dbphotos = new Db_Photos();
        public List<Photos> Search(int photo_id)
        {
            try
            {
                return dbphotos.Search(photo_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据地址查找id
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public int SearchFormAdress(string adress)
        {
            try
            {
        
                return dbphotos.SearchFormAdress(adress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Photos> searchAlbumPhotos(int id)
        {
            try
            {
                return dbphotos.SearchAlbumPhotos(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<string> SearchTopNum(int id,int n)
        {
            try
            {
                return dbphotos.SearchTopNum(id, n);
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
                dbphotos.DeleteFromAlbumID(AlbumID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}