using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
namespace NewRLWeb.Package
{
    public class Logic_Team_members
    {
        private Db_Team_members dbmembers = new Db_Team_members();
        public List<Members> search(int projectI_id)
        {
            try
            {
                return dbmembers.search(projectI_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}