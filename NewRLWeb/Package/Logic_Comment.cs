using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;

namespace NewRLWeb.Package
{
    public class Logic_Comment
    {
        private Db_Comment dbcom = new Db_Comment();
        /// <summary>
        /// 获取相册评论
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<Commment> GetComment(int typeId,string Type)
        {
            try
            {
                return dbcom.GetComment(typeId, Type);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddModel(Commment model)
        {
            try
            {
                return dbcom.AddModel(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}