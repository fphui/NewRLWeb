using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using NewRLWeb.Models;
using System.Data.Entity;

namespace NewRLWeb.Common
{
    public class Db_Users : BaseDb
    {

        /// <summary>
        /// 设置唯一ID---2017.1.15   zsl
        /// change by llt 2017.2.13
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public string AddUnique_ID(string name, string native)
        {
            try
            {
                string U_ID = "";
                if (SearchNum() > 0)
                {
                    var query = (from o in context.users
                                 where o.Username == name && o.Native == native
                                 select o.Unique_ID).Count();
                    if (query == 0)
                    {
                        //var querys = (from o in context.users
                        //              select o.Unique_ID).Max();
                        //U_ID = (double.Parse(querys) + 1).ToString();
                        string timenow = DateTime.Now.ToString("yyyyMMddhhmmss");//得到系统时间
                        U_ID = timenow;
                    }
                    else
                    {
                        U_ID = (from o in context.users
                                where o.Username == name && o.Native == native
                                select o.Unique_ID).First().ToString();
                    }
                }
                else
                    U_ID = "1";
                return U_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool HasUser(string id)
        {
            try
            {
                var query = from o in context.users//用linq语句 查询实体模型类中T_UserInfo表  
                            where o.UserID == id
                            select o;
                if (query == null || !query.Any())
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool IsUser(string name, string uniqueid)
        {
            try
            {
                var query = from o in context.users//用linq语句 查询实体模型类中T_UserInfo表  
                            where o.Username == name && o.Unique_ID == uniqueid
                            select o;
                if (query == null || !query.Any())
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Users ContainUser(string account)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Username == account || o.UserID == account || o.E_mail == account
                                   select o
                  ).FirstOrDefault();
                return queryResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据账号、密码 判断用户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Users HasUser(string account, string password)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.UserID == account
                                   select o
                  ).ToList();
                //if (!String.IsNullOrEmpty(password))
                var q = queryResult.Where(p => p.Password == password).FirstOrDefault();
                return q;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public List<Users> Search()
        {
            try
            {
                var queryResult = (from o in context.users
                                   select o
                                  ).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据学号查询用户信息
        /// </summary>
        /// <param name="unique_id"></param>
        /// <returns></returns>
        public Users Search(string userID)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.UserID == userID
                                   select o).FirstOrDefault();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 名字查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Users SearchbyName(string name)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Username == name
                                   select o).SingleOrDefault();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 邮箱查询
        /// </summary>
        /// <param name="e_mail"></param>
        /// <returns></returns>
        public Users SearchbyMail(string e_mail)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.E_mail == e_mail
                                   select o).SingleOrDefault();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// 邢亚男 2017/02/23  按照type查看毕业生
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //public List<Users> SearchByType(string type)
        //{
        //    try
        //    {
        //        var queryResult = (from o in context.users
        //                           where o.Education == type
        //                     orderby o.Publicationtime descending
        //                     select o).ToList();
        //        return queryResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<Users> SearchByType(byte education)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Education == education
                                   select o
                                  ).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Users> SearchByTypetime(int? data)
        {
            // Convert.ToString(
            try
            {

                if (data != 0 && data != 1)//按年份取数据
                {
                    var queryResult = (from o in context.users
                                       where o.Graduate_Data.Value.Year == data
                                       select o
                                  ).ToList();
                    return queryResult;
                }
                else//按毕业生类别取数据
                {
                    var queryResult = (from o in context.users
                                       where o.Graduate_Data != null && o.Education == data
                                       select o
                                  ).ToList();
                    return queryResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //}
        /// <summary>
        /// 根据（年份、教育类型、地址、名字）查询多个毕业生信息
        /// </summary>
        /// <param name="raduate_data"></param>
        /// <param name="education"></param>
        /// <param name="graduate_address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Users> Search(string graduate_data, string education, string graduate_address, string name)
        {
            try
            {
                var queryResult = (from o in context.users
                                   select o
                                  ).ToList();
                if (!String.IsNullOrEmpty(education))
                    queryResult = queryResult.Where(p => p.Education == Convert.ToByte(education)).ToList();
                if (graduate_data != "")
                    queryResult = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == graduate_data).ToList();
                if (!String.IsNullOrEmpty(graduate_address))
                    queryResult = queryResult.Where(p => p.Graduate_Address == graduate_address).ToList();
                if (!String.IsNullOrEmpty(name))
                    queryResult = queryResult.Where(p => p.Username == name).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据（教育类型、地址、名字）查询多个毕业生信息
        /// </summary>
        /// <param name="education"></param>
        /// <param name="graduate_address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Users> Search(byte education, string graduate_address, string name)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Education == education
                                   select o
                                  ).ToList();
                //if (education != null)
                //    queryResult = queryResult.Where(p => p.Education == education).ToList();
                if (graduate_address != null)
                    queryResult = queryResult.Where(p => p.Graduate_Address == graduate_address).ToList();
                if (name != null)
                    queryResult = queryResult.Where(p => p.Username == name).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据（毕业年份、地址、名字）查询多个毕业生信息
        /// </summary>
        /// <param name="education"></param>
        /// <param name="graduate_address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        //public List<Users> Search(DateTime raduate_data, string graduate_address, string name)
        //{
        //    try
        //    {
        //        var queryResult = (from o in context.users
        //                           select o
        //                          ).ToList();
        //        if (raduate_data != null)
        //            queryResult = queryResult.Where(p => p.Graduate_Data.Year == raduate_data.Year).ToList();
        //        if (graduate_address != null)
        //            queryResult = queryResult.Where(p => p.Graduate_Address == graduate_address).ToList();
        //        if (name != null)
        //            queryResult = queryResult.Where(p => p.Username == name).ToList();
        //        return queryResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// 根据（地址、名字）查询多个毕业生信息
        /// </summary>
        /// <param name="graduate_address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Users> Search(string graduate_address, string name)
        {
            try
            {
                var queryResult = (from o in context.users
                                   select o
                                  ).ToList();
                if (graduate_address != null)
                    queryResult = queryResult.Where(p => p.Graduate_Address == graduate_address).ToList();
                if (name != null)
                    queryResult = queryResult.Where(p => p.Username == name).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询毕业生的年份
        /// </summary>
        /// <returns></returns>
        public List<DateTime?> SearchYear()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Data != null
                                   orderby o.Graduate_Data descending
                                   select o.Graduate_Data).Distinct().ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///所有毕业生集合
        /// </summary>
        public List<Users> SearchGraduatesInfo()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Data != null
                                   select o).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 所有非毕业生集合---2017.1.14 zsl
        /// </summary>
        /// <returns></returns>
        public List<Users> SearchNotGraduatesInfo()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Data == null
                                   select o).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 毕业总人数
        /// </summary>
        /// <returns></returns>
        public int SearchAllGraduates()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Data != null
                                   select o).Count();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Users> SearchGraduates()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Data != null
                                   orderby o.Graduate_Data descending
                                   select o).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取应该毕业的用户
        /// add by llt 2017.2.6
        /// </summary>
        /// <returns></returns>
        public List<Users> SearchShouldGra()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Data == null && ((o.Education == 0 && (DateTime.Now.Year - o.Admission_Data.Year) >= 4) || (o.Education == 1 && (DateTime.Now.Year - o.Admission_Data.Year) >= 3))
                                   select o).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 本科生毕业总人数
        /// </summary>
        /// <returns></returns>
        public int SearchUGraduates()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Data != null && o.Education == 0)
                                   select o).Count();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 研究生毕业总人数
        /// </summary>
        /// <returns></returns>
        public int SearchYGraduates()
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Data != null && o.Education == 1)
                                   select o).Count();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 当年本科生毕业总人数
        /// </summary>
        /// <returns></returns>
        public int SearchUGraduatesByYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Data.Value.Year == year && o.Education == 0)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 当年研究生毕业总人数
        /// </summary>
        /// <returns></returns>
        public int SearchYGraduatesByYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Data.Value.Year == year && o.Education == 1)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 当年工作人数
        /// </summary>
        /// <returns></returns>
        public int SearchWorkbyYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Direction == "工作" && o.Graduate_Data.Value.Year == year)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 当年本科生工作人数
        /// </summary>
        /// <returns></returns>
        public int SearchUWorkbyYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Direction == "工作" && o.Education == 0 && o.Graduate_Data.Value.Year == year)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 当年研究生工作人数
        /// </summary>
        /// <returns></returns>
        public int SearchYWorkbyYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Direction == "工作" && o.Education == 1 && o.Graduate_Data.Value.Year == year)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 柱状图 BLM
        ///<summary>
        ///最新入学年份
        ///2017.03.01 BLM修改 增加无未毕业学生的判断
        ///</summary>
        ///<returns></returns>
        public int latestAdmissionYear()
        {
            try
            {
                int MaxYear;
                var queryYear = (from o in context.users
                                 where (o.Graduate_Data == null)
                                 group o by o.Admission_Data into qy
                                 select new
                                 {
                                     qy.Key,
                                     Num = qy.Count(),
                                 }).OrderByDescending(x => x.Key.Year).ToList();
                //2017.03.01 start
                if (queryYear.Count() != 0)
                {
                    MaxYear = queryYear.Max(x => x.Key).Year;//取最低年级入学时间
                }
                else
                {
                    MaxYear = 0;
                }
                //2017.03.01 end
                return MaxYear;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///本科各年级人数
        /// </summary>
        ///<returns></returns>
        public List<int> BEachClass()
        {
            try
            {
                List<int> BEachClassNum = new List<int>();
                var queryYear = (from o in context.users
                                 where (o.Education == 0 && o.Graduate_Data == null)
                                 group o by o.Admission_Data into qy//未毕业
                                 select new
                                 {
                                     qy.Key,
                                     Num = qy.Count(),
                                 }).OrderByDescending(x => x.Key.Year).ToList();
                var MaxYear = latestAdmissionYear();//取最低年级入学时间
                for (int i = 0; i < 3; MaxYear--, i++)
                {
                    if (queryYear.Count() <= i)
                        BEachClassNum.Add(0);
                    else
                    {
                        if (queryYear[i].Key.Year == MaxYear)
                            BEachClassNum.Add(queryYear[i].Num);
                        else
                            BEachClassNum.Add(0);
                    }
                }
                return BEachClassNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///研究生各级人数
        /// </summary>
        /// <return></return>
        public List<int> YEachClass()
        {
            try
            {
                List<int> YEachClassNum = new List<int>();
                var queryYear = (from o in context.users
                                 where (o.Education == 1 && o.Graduate_Data == null)
                                 group o by o.Admission_Data into qy//未毕业
                                 select new
                                 {
                                     qy.Key,
                                     Num = qy.Count()
                                 }).OrderByDescending(x => x.Key.Year).ToList();
                var MaxYear = latestAdmissionYear();//取最低年级入学时间
                for (int i = 0; i < 3; MaxYear--, i++)
                {
                    if (queryYear.Count() <= i)
                        YEachClassNum.Add(0);
                    else
                    {
                        if (queryYear[i].Key.Year == MaxYear)
                            YEachClassNum.Add(queryYear[i].Num);
                        else
                            YEachClassNum.Add(0);
                    }
                }
                return YEachClassNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 当年考研人数
        /// </summary>
        /// <returns></returns>
        public int SearchYbyYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Direction == "考研" && o.Graduate_Data.Value.Year == year)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 当年考博人数
        /// </summary>
        /// <returns></returns>
        public int SearchBbyYear(int year)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where (o.Graduate_Direction == "考博" && o.Graduate_Data.Value.Year == year)
                                   select o).ToList();
                int q = queryResult.Where(p => p.Graduate_Data.ToString().Substring(0, 4) == year.ToString()).Count();
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 省份分布人数
        /// </summary>
        /// <returns></returns>
        public int SearchbyProvince(string province)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Address == province
                                   select o).Count();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///各省份毕业人数
        ///</summary>
        ///<returns></returns>
        public int SearchbyProvinceAndGraduate(string province)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.Graduate_Address == province && o.Graduate_Data != null
                                   select o).Count();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 总人数
        /// add by llt 2017.2.3
        /// </summary>
        /// <returns></returns>
        public int SearchNum()
        {
            try
            {
                var query = (from o in context.users
                             select o).Count();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取用户绑定id
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public string GetUniqueID(string Userid)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.UserID == Userid
                                   select o.Unique_ID).First();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public string GetUserPhoto(string Userid)
        {
            try
            {
                var queryResult = (from o in context.users
                                   where o.UserID == Userid
                                   select o.Photo_Address).ToString();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 毕业生管理
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(Users user)
        {
            try
            {
                context.users.Add(user);
                if (context.SaveChanges() >= 1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Save(Users user)
        {
            try
            {
                var entry = context.Entry(user);
                if (entry.State == EntityState.Detached)
                {
                    var set = context.Set<Users>();
                    Users attachedProduct = set.Local.SingleOrDefault(p => p.UserID == user.UserID);

                    //如果已经被上下文追踪
                    if (attachedProduct != null)
                    {
                        var attachedEntry = context.Entry(attachedProduct);
                        attachedEntry.CurrentValues.SetValues(user);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Users user)
        {
            try
            {
                var model = (from o in context.users
                             where o.UserID == user.UserID
                             select o).Single();
                //context.Entry(ae).State = EntityState.Modified;
                model.Unique_ID = user.Unique_ID;
                model.Username = user.Username;
                model.Password = user.Password;
                model.Sex = user.Sex;
                model.Photo_Address = user.Photo_Address;
                model.QQ = user.QQ;
                model.Subject = user.Subject;
                model.Phone_Number = user.Phone_Number;
                model.Address = user.Address;
                model.Admission_Data = user.Admission_Data;
                model.AlbumID = user.AlbumID;
                model.E_mail = user.E_mail;
                model.Education = user.Education;
                model.Graduate_Address = user.Graduate_Address;
                model.Graduate_Data = user.Graduate_Data;
                model.Graduate_Direction = user.Graduate_Direction;
                model.Learning_experience = user.Learning_experience;
                model.Message = user.Message;
                model.Native = user.Native;
                return context.SaveChanges() >= 1 ? true : false;
                //context.Entry(user).State = EntityState.Modified;
                //context.Configuration.ValidateOnSaveEnabled = false;
                //if (context.SaveChanges() >= 1)
                //{
                //    context.Configuration.ValidateOnSaveEnabled = true;
                //    return true;
                //}
                //context.Configuration.ValidateOnSaveEnabled = true;
                //return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(string userID, DateTime? date)
        {
            try
            {
                var model = (from o in context.users
                             where o.UserID == userID
                             select o).Single();
                if (date != null)
                {
                    model.Graduate_Data = date;
                    context.Configuration.ValidateOnSaveEnabled = false;
                    if (context.SaveChanges() >= 1)
                    {
                        context.Configuration.ValidateOnSaveEnabled = true;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                Users user = context.users.Find(id);
                context.users.Remove(user);
                if (context.SaveChanges() >= 1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Users Find(string id)
        {
            Users user = context.users.Find(id);
            return user;
        }
        public bool Create(Users user)
        {
            try
            {
                context.users.Add(user);
                if (context.SaveChanges() >= 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新相册id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="AlubmID"></param>
        /// <returns></returns>
        public bool UpdateAlbumId(string name, int AlubmID)
        {
            try
            {
                var album = (from o in context.users
                             where o.Unique_ID == name
                             select o).First();
                album.AlbumID = AlubmID;
                context.Configuration.ValidateOnSaveEnabled = false;
                if (context.SaveChanges() >= 1)
                {
                    context.Configuration.ValidateOnSaveEnabled = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                context.Configuration.ValidateOnSaveEnabled = true;
                throw ex;

            }
            return false;

        }

        ///// <summary>
        ///// 分页查询+特定个数
        ///// add by llt 2017.2.6
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="size"></param>
        ///// <returns></returns>
        //public List<Users> SearchPaging(int page, int size)
        //{
        //    try
        //    {
        //        var queryResult = (from o in context.users
        //                           orderby o.Admission_Data descending
        //                           select o
        //                    ).Take(size * page).Skip(size * (page - 1)).ToList();
        //        return queryResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}