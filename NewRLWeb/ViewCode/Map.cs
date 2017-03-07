using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.ViewModels;
using NewRLWeb.Package;
using NewRLWeb.Helpers;

namespace NewRLWeb.ViewCode
{
    public class Map
    {
        //private ViewModels.Map map = new ViewModels.Map();
        private Logic_Users users = new Logic_Users();
        private List<ViewModels.Map> provinces = new List<ViewModels.Map>();
        public List<ViewModels.Map> GraduOfProvincelist()
        {
            // = new List<Province.GraduOfProvince>();  //获取每个省的毕业生人数
            foreach (string ad in Province.Provinces)
            {
                ViewModels.Map map = new ViewModels.Map();
                map.Province=ad;
                map.numOfGraduate=users.SearchbyProvinceAndGraduate(ad);
                provinces.Add(map);
                //provinces.Add(new Province.GraduOfProvince(ad, users.SearchbyProvinceAndGraduate(ad)));
            }
            return provinces;
        }
    }
}