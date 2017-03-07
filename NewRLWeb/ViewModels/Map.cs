using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using NewRLWeb.Helpers;
namespace NewRLWeb.ViewModels
{
    public class Map
    {
        //public List<Province.GraduOfProvince> provinces { get; set; }
        public string Province { get; set; }
        public int numOfGraduate { get; set; }
    }
}