using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.Common
{
    public class Db_Members:BaseDb
    {
        public List<Members> Search(int id)
        {
            var queraquestResult = (from o in context.members
                                    where o.ProjectID == id
                                    select o
                                   ).ToList();
            return queraquestResult;
        }
    }
}