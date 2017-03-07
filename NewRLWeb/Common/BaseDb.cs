using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.Common
{
    public abstract class BaseDb
    {
        public static rlwzContext context = new rlwzContext();
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool AddModel(object model)
        {
            return true;
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool DeleteModel(object key)
        {
            return true;
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="oldmodel"></param>
        /// <param name="newmodel"></param>
        /// <returns></returns>
        public virtual bool UpdateModel(object key, object newmodel)
        {
            return true;
        }
        /// <summary>
        /// 通过主键得到一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual object GetModelByKey(object key)
        {
            return null;
        }

    }
}