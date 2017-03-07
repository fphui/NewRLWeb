using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
using System.Data;
namespace NewRLWeb.Common
{
    public class Db_Rules_Management:BaseDb
    {
        public Rules_Management search()
        {
            try
            {
                var questResult = (from o in context.rules_management
                                   select o
                                   ).FirstOrDefault();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool save(Rules_Management rules_management)
        {

            try {
                var entry = context.Entry(rules_management);
                rules_management.Publicationtime = DateTime.Now;
                rules_management.Coverage = "~/Html/Rules/" + rules_management.Coverage;
                if (entry.State == EntityState.Detached)
                {
                    var set = context.Set<Rules_Management>();
                    Rules_Management attachedProduct = set.Local.SingleOrDefault(p => p.ID == rules_management.ID);

                    //如果已经被上下文追踪
                    if (attachedProduct != null)
                    {
                        var attachedEntry = context.Entry(attachedProduct);
                        attachedEntry.CurrentValues.SetValues(rules_management);
                    }
                    else //如果不在当前上下文追踪
                    {
                        entry.State = EntityState.Modified;
                    }
                }
                if (context.SaveChanges() >= 1)
                    return true;
                return false;
            }
            catch(Exception e)
            {
                return false;
                throw e;
                
            }
        }
    }
}