using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;
namespace NewRLWeb.ViewCode
{
    /// <summary>
    /// 相册集主页
    /// </summary>
    public class vc_Album
    {
        private Album alb = new Album();
        private Logic_Users user = new Logic_Users();
        private Logic_Album album = new Logic_Album();

        /// <summary>
        /// 创建个人相册
        /// </summary>///

        public string CreatePersonAlbum(string UniqueID)
        {          
           // string UniqueID=user.GetUniqueID(userid);
            alb.Albumname= UniqueID ;
            alb.Album_Type = "个人";
            alb.Publicationtime= DateTime.Now;
            alb.Folderpath = "/Images/Album/" + UniqueID;
            alb.Pho_Address="asddasda";
           
            try
            { 
            album.Create(alb);
                int temp=album.SearchFromNameToID(UniqueID);
            user.UpdateAlbumId(UniqueID,temp );
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return UniqueID;
        }
    }
}