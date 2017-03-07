using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NewRLWeb.Models
{
    public class rlwzContext : DbContext
    {

        //public rlwzContext()
        //    : base("DefaultConnection")
        //{

        //}
        public DbSet<Administrator> administrator { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Employment_Information> employment_information { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Commment> comment { get; set; }
        public DbSet<Album> album { get; set; }
        public DbSet<Photos> photos { get; set; }
        public DbSet<Notice> notice { get; set; }
        public DbSet<Project_Case> project_case { get; set; }
        public DbSet<Rules_Management> rules_management { get; set; }
        public DbSet<Members> members { get; set; }
        public DbSet<Large_type> large_type { get; set; }
        public DbSet<Small_Type> small_type { get; set; }
        public DbSet<Academic_Exchange> academic_exchange { get; set; }
    }
}