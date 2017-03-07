using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NewRLWeb.Models;

namespace NewRLWeb.ViewModels
{
    public class vmContext:DbContext
    {
        public DbSet<Graduate> graduate { get; set; }
        public DbSet<ProjectCase> project { get; set; }
        public DbSet<UserLogin> userlogin { get; set; }
        public DbSet<News_Notices> newnotice { get; set; }
        public DbSet<UploadImageModel> UploadImage { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<EmailTo> EmailTo { get; set; }
    }
}