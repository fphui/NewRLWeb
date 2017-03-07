using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;

namespace NewRLWeb.ViewCode
{
    public class Comment
    {
        List<ViewModels.vm_Comment> com = new List<vm_Comment>();
        private Logic_Users user = new Logic_Users();
        private Logic_Comment Comm = new Logic_Comment();
        public List<ViewModels.vm_Comment> GetComment(int TypeID,string Type)
        {
            try
            {

                List<Commment> list = new List<Commment>();
                list = Comm.GetComment(TypeID,Type);
                List<string> str = new List<string>();
                foreach (var i in list)
                {
                    str.Add(user.GetUserPhoto(i.UserID));
                }
                for (int i = 0; i < list.Count; i++)
                {
                    foreach (var item in com)
                    {
                        item.Comment = list[i];
                        item.UserPhoto = str[i];
                    }
                }
                return com;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}