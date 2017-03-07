using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.Common
{
    public class Db_Comment:BaseDb
    {
        public List<Commment> GetComment(int typeID,string Type)
        {
            try
            {
                var queryResult = (from o in context.comment
                                   where o.Type_ID == typeID && o.Large_Type == Type
                                   select o).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override bool AddModel(object model)
        {
            try
            {
                Commment com = (Commment)model;
                context.comment.Add(com);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        } 
    }
}