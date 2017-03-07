using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;
namespace NewRLWeb.ViewCode
{
    /// <summary>
    /// 毕业去向主页
    /// </summary>
    public class Graduate
    {
        ViewModels.Graduate gra = new ViewModels.Graduate();
        private Logic_Users user = new Logic_Users();
        private Logic_Employment_information graduete = new Logic_Employment_information();
        private Logic_Photos photos = new Logic_Photos();
        
        /// <summary>
        /// 查找毕业生个人详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewModels.Graduate Search(string id)
        {
            try
            {
                gra.Person = user.Search(id);
                gra.Informations = graduete.Search(gra.Person.Unique_ID);
                int album_id = gra.Person.AlbumID;
                gra.photos = photos.Search(album_id);
                
                return gra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}