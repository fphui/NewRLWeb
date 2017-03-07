using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewRLWeb.Package;
using NewRLWeb.ViewModels;
using NewRLWeb.Models;
using NewRLWeb.Common;
namespace NewRLWeb.ViewCode
{
    /// <summary>
    /// 项目案例主页
    /// </summary>
    public class Project
    {
        private Logic_Project_Case project = new Logic_Project_Case();
        private Logic_Members member = new Logic_Members();
        private vmContext vm = new vmContext();
        private rlwzContext rl = new rlwzContext();
        /// <summary>
        /// 返回一个包括案例及成员的model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectCase search(int id)
        {
            Project_Case projectCase = rl.project_case.Find(id);
            List<Members> members = member.Search(projectCase.ProjectID);
            ProjectCase project = new ProjectCase();
            project.pojectcase = projectCase;
            project.member = members;
            return project;
        }
    }
}