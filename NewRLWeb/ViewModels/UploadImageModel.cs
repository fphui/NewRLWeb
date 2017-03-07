using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Models;

namespace NewRLWeb.ViewModels
{
    public class UploadImageModel
    {
        public string headFileName { get; set; }

        public int x { get; set; }


        public int y { get; set; }


        public int width { get; set; }


        public int height { get; set; }

        public string userID { get; set; }

        public string address { get; set; }
    }
}