using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;
namespace NewRLWeb.Common
{
    public class Db_Small_Type:BaseDb
    {
        public List<Small_Type> Search(string l_type)
        {
            try
            {
                var questResult = (from o in context.small_type
                                   where o.L_Type == l_type
                                   select o
                                   ).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Small_Type> SearchNewsType(string l_type)
        {
            try
            {
                var questResult = (from o in context.small_type
                                   where o.L_Type == l_type && o.S_Type != "个人"
                                   select o
                                   ).ToList();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Small_Type SearchbyID(int id)
        {
            try
            {
                var questResult = (from o in context.small_type
                                   where o.ID == id
                                   select o
                                   ).SingleOrDefault();
                return questResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add(Small_Type s_type)
        {
            string temp = s_type.S_Type;
            try
            {
                var query = (from o in context.small_type
                             where o.S_Type == temp
                             select o).ToList();                          
                if(query.Count() == 0)
                    context.small_type.Add(s_type);
                return context.SaveChanges()>0?true:false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string ltype)
        {
            try
            {
                var query = (from o in context.small_type
                             where o.L_Type == ltype
                             select o).ToList();
                int num = query.Count();
               for(int i=0;i<query.Count();i++)
               {
                   context.small_type.Remove(query.First());
                   if (context.SaveChanges() >= 1)
                       num -= 1;
               }
               if (num == 0)
                   return true;
               else
                   return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var query = (from o in context.small_type
                             where o.ID == id
                             select o).SingleOrDefault();
                context.small_type.Remove(query);
                return context.SaveChanges() >= 1 ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangeRetain(int id,byte retain)
        {
            try
            {
                var query = (from o in context.small_type
                             where o.ID == id
                             select o).First();
                query.Retain = retain;
                return context.SaveChanges() >= 1 ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}