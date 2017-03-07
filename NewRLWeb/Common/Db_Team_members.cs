using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Team_members:BaseDb
    {
        public List<Members> search(int projectI_id)
        {
            try
            {
                var questResult = (from o in context.members
                                   where o.ProjectID == projectI_id
                                   select o
                                   ).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}