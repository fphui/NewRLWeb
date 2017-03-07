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
    public class ProjectCase
    {
        public List<Members> member { get; set; }
        public Project_Case pojectcase { get; set; }
    }
}