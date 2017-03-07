using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewRLWeb.Helpers
{
    public class Province
    {
        public static List<string> province = new List<string>()
        {
            {"澳门"}, 
            {"西藏"}, 
            {"甘肃"}, 
            {"青海"}, 
            {"内蒙古"}, 
            {"黑龙江" }, 
            {"吉林" }, 
            {"辽宁"}, 
            {"山东"}, 
            {"河北"}, 
            {"山西"}, 
            {"北京"}, 
            {"天津"}, 
            {"陕西"}, 
            {"宁夏"}, 
            {"河南"}, 
            {"江苏"}, 
            {"安徽"}, 
            {"上海"}, 
            {"浙江"}, 
            {"江西"}, 
            {"福建"}, 
            {"广东"}, 
            {"海南"}, 
            {"广西"}, 
            {"贵州"}, 
            {"云南"}, 
            {"四川"}, 
            {"重庆"}, 
            {"湖南" }, 
            {"湖北"}, 
            {"台湾" }, 
            {"香港" }, 
            {"新疆"}
        };

        public static IEnumerable<string> Provinces
        {
            get
            {
                return province;
            }
        }
        //public struct GraduOfProvince
        //{
        //    public string Province;
        //    public int numOfGraduate;
        //    public string Pro
        //    {
        //        get { return Province; }
        //        set { Province = value; }
        //    }
        //    public int NumofGraduate
        //    {
        //        get { return numOfGraduate; }
        //        set { numOfGraduate = value; }
        //    }
        //    public GraduOfProvince(string _Province, int _numofGraduate)
        //    {
        //        this.Province = _Province;
        //        this.numOfGraduate = _numofGraduate;
        //    }
        //}
        
        
    }
}