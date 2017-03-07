using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;

namespace NewRLWeb.Package
{
    public class Logic_Members
    {
        private Db_Members members = new Db_Members();
        public List<Members> Search(int id)
        {
            try
            {
                return members.Search(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}