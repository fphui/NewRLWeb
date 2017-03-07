using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using NewRLWeb.Models;

namespace NewRLWeb.ViewModels
{
    public class vm_Comment
    {
        public Commment Comment { get; set; }
        
        public string UserPhoto { get; set; }

        public Photos A_Photos { get; set; }
    }
}