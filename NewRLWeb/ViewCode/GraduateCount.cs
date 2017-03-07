//王刚添加：对毕业生毕业状况进统计
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.Models;
using NewRLWeb.ViewModels;

namespace NewRLWeb.ViewCode
{
    public class GraduateCount
    {
        List<ViewModels.GraduateCount> gra = new List<ViewModels.GraduateCount>();
        List<DateTime?> date = new List<DateTime?>();
        List<int> years = new List<int>();
        private Logic_Users user = new Logic_Users();
        public List<ViewModels.GraduateCount> Search()
        {
            try
            {
                date = user.SearchYear();
                foreach(DateTime a in date)
                {
                    years.Add(a.Year);
                }
                foreach (int ad in years.Distinct())
                {
                    ViewModels.GraduateCount g = new ViewModels.GraduateCount();
                    g.GraduYears = ad;
                    g.GraduUWorknum = user.SearchUWorkbyYear(ad);
                    g.GraduYWorknum = user.SearchYWorkbyYear(ad);
                    g.GraduYnum=user.SearchYbyYear(ad);
                    g.GraduBnum=user.SearchBbyYear(ad);
                    g.UGraduates=user.SearchUGraduatesByYear(ad);
                    g.YGraduates=user.SearchYGraduatesByYear(ad);
                    gra.Add(g);
                }
                return gra;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}