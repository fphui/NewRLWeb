using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Common;
using NewRLWeb.Models;
using System.IO;
namespace NewRLWeb.Package
{
    public class Logic_Users
    {

        private Db_Users dbuser = new Db_Users();


        public string AddUnique_ID(string name, string native)
        {
            try
            {
                return dbuser.AddUnique_ID(name, native);
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
                return dbuser.HasUser(id);
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
                return dbuser.IsUser(name, uniqueid);
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
                return dbuser.ContainUser(account);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return dbuser.HasUser(account, password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Users> Search()
        {
            try
            {
                return dbuser.Search();
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
                return dbuser.Search(userID);
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
                return dbuser.SearchbyName(name);
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
                return dbuser.SearchbyMail(e_mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据（年份、教育类型、地址、名字）查询多个毕业生信息
        /// </summary>
        /// <param name="raduate_data"></param>
        /// <param name="education"></param>
        /// <param name="graduate_address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Users> Search(string raduate_data, string education, string graduate_address, string name)
        {
            try
            {
                //byte edu = 0;
                //switch (education)
                //{
                //    case "本科生": edu = 0;
                //        break;
                //    case "研究生": edu = 1;
                //        break;
                //    default: return dbuser.Search(raduate_data, graduate_address, name);
                //}  
                return dbuser.Search(raduate_data, education, graduate_address, name);
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
        public List<Users> Search(string education, string graduate_address, string name)
        {
            try
            {
                byte edu = 0;
                switch (education)
                {
                    case "本科生": edu = 0;
                        break;
                    case "研究生": edu = 1;
                        break;
                    default: return dbuser.Search(graduate_address, name);
                }
                return dbuser.Search(edu, graduate_address, name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //邢亚男  2017/02/24 类型时间
        public List<Users> SearchByType(byte type)
        {
            try
            {
                return dbuser.SearchByType(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Users> SearchByTypetime(int? time)
        {
            try
            {
                return dbuser.SearchByTypetime(time);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询有毕业的年份
        /// </summary>
        /// <returns></returns>
        public List<DateTime?> SearchYear()
        {
            try
            {
                return dbuser.SearchYear();
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
                return dbuser.SearchGraduatesInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 所有非毕业生集合---2017.1.14  zsl
        /// </summary>
        /// <returns></returns>
        public List<Users> SearchNotGraduatesInfo()
        {
            try
            {
                return dbuser.SearchNotGraduatesInfo();
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
                return dbuser.SearchAllGraduates();
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
                return dbuser.SearchGraduates();
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
                return dbuser.SearchShouldGra();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分页查询/特定个数
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<Users> SearchPaging(List<Users> gra, int page, int size)
        {
            try
            {
                if (page == 1 && size >= gra.Count())
                {
                    return gra;
                }
                return gra.Take(size * page).Skip(size * (page - 1)).ToList();
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
                return dbuser.SearchUGraduates();
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
                return dbuser.SearchYGraduates();
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
                return dbuser.SearchUGraduatesByYear(year);
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
                return dbuser.SearchYGraduatesByYear(year);
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
                return dbuser.SearchWorkbyYear(year);
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
                return dbuser.SearchUWorkbyYear(year);
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
                return dbuser.SearchYWorkbyYear(year);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 柱状图 BLM
        ///<summary>
        ///本科生各年级人数
        /// </summary>
        /// <returns></returns>
        public List<int> BEachClass()
        {
            try
            {
                //int NCcount = 0;//记录当前年级人数
                //List<int> num = new List<int>();//每一级人数
                //List<int> allBY = new List<int>();//所有本科生入学年份
                //allBY = dbuser.BSortByYear();
                //int NowY = allBY[0];//NowY定位
                //for (int i = 0; i < allBY.Count() && num.Count() < 3; i++)
                //{
                //    if (NowY == allBY[i])
                //    {
                //        NCcount++;
                //    }
                //    else
                //    {
                //        NowY++;
                //        num.Add(NCcount);
                //        NCcount = 0;
                //    }
                //}
                return dbuser.BEachClass();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///研究生各级人数
        ///</summary>
        ///<returns></returns>
        public List<int> YEachClass()
        {
            try
            {
                //int NCcount = 0;//记录当前年级人数
                //List<int> numY = new List<int>();//每一级人数
                //List<int> allBY = new List<int>();//所有研究生入学年份
                //allBY = dbuser.YSortByYear();
                //int NowY = allBY[0];//NowY定位
                //for (int i = 0; i < allBY.Count() && numY.Count() <= 3; i++)
                //{
                //    if (NowY == allBY[i])
                //    {
                //        NCcount++;
                //    }
                //    else
                //    {
                //        NowY++;
                //        numY.Add(NCcount);
                //        NCcount = 0;
                //    }
                //}
                return dbuser.YEachClass();
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
                return dbuser.SearchYbyYear(year);
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
                return dbuser.SearchBbyYear(year);
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
                return dbuser.SearchbyProvince(province);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 各省份毕业人数
        /// </summary>
        /// <returns></returns>
        public int SearchbyProvinceAndGraduate(string province)
        {
            try
            {
                return dbuser.SearchbyProvinceAndGraduate(province);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetUserPhoto(string UserID)
        {
            try
            {
                return dbuser.GetUserPhoto(UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Add(Users user)
        {
            try
            {
                return dbuser.Add(user);
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
                return dbuser.Save(user);
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
                return dbuser.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Users Find(string id)
        {
            try
            {
                return dbuser.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(Users user)
        {
            try
            {
                return dbuser.Create(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Change(IEnumerable<HttpPostedFileBase> files, string pathForSaving)
        {
            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        DateTime dt = DateTime.Now;
                        string realName = file.FileName;
                        var path = Path.Combine(pathForSaving, realName);
                        file.SaveAs(path);
                        return true;
                        //   rules_management.Coverage = file.FileName;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool Update(Users user)
        {
            try
            {
                return dbuser.Update(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(string userID,DateTime? date)
        {
            try
            {
                return dbuser.Update(userID,date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取用户UniqueID
        /// </summary>
        /// <returns></returns>
        public string GetUniqueID(string Userid)
        {
            try
            {
                return dbuser.GetUniqueID(Userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //更新AlbumID
        public bool UpdateAlbumId(string name,int AlubmID )
        {
            try
            {
                dbuser.UpdateAlbumId(name , AlubmID );

                return true;
            }
            catch(Exception ex)
            {
                throw ex;

            }


        }

    }
}