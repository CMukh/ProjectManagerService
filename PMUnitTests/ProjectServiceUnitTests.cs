using NUnit.Framework;
using ProjectManagerBL;
using ProjectManagerDL;
using ProjectManagerServices.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace PMUnitTests
{
    class ProjectServiceUnitTests
    {
        ProjectController projectController;
        ProjectDAO ProjectDAO;

        [SetUp]
        public void Init()
        {
            ProjectDAO = new ProjectDAO();
            projectController = new ProjectController(ProjectDAO);
            DeleteAllprojects();

        }
        private void DeleteAllprojects()
        {
            //Delete All tasks
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            taskDBEntities.Database.ExecuteSqlCommand("delete from users");

            taskDBEntities.Database.ExecuteSqlCommand("delete from project");
            taskDBEntities.Database.ExecuteSqlCommand("delete from taskdetails");
        }
        private int GetProjID(int index)
        {
            //GetAllTasks
            IHttpActionResult response = projectController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<ProjectViewModel>>;
            var t = contentResult.Content;

            //Count
            return t[index].ProjectID;
        }
        private void AddNewProject()
        {
            ProjectViewModel p = new ProjectViewModel();
            //Add Project//
            p.ProjectName = "Project1";
            p.EndDate = "2018-09-13";
            p.StartDate = "2018-09-12";
            p.Priority = 10;
            p.Status = "Running";
            projectController.Post(p);

        }

        [Test]
        public void Get()
        {
            //Add Project//
            AddNewProject();

            //GetAllTasks
            IHttpActionResult response = projectController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<ProjectViewModel>>;
            var t = contentResult.Content;

            //Count
            Assert.IsTrue(t.Where(p => p.ProjectName == "Project1").Count() == 1);
        }

        [Test]
        public void GetProject()
        {
            //Add Task//
            AddNewProject();
            var t = projectController.Get(GetProjID(0));
            Assert.IsTrue(t.ProjectName == "Project1");
        }
        [Test]
        public void AddProject()
        {
            //Add Task//
            AddNewProject();

            //Check if Added//
            IHttpActionResult response = projectController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<ProjectViewModel>>;
            var t = contentResult.Content;
            Assert.IsTrue(t.Where(p => p.ProjectName == "Project1").Count() == 1);
        }

        [Test]
        public void DeleteProject()
        {
            //Add 2 Tasks//
            AddNewProject();


            //Delete 1 task//
            projectController.Delete(GetProjID(0));

            //Check if Deleted// There should be 1 task
            IHttpActionResult response = projectController.Get();
            var contentResult = response as NotFoundResult;


            Assert.IsNotNull(contentResult);
        }

        [Test]
        public void Editproject()
        {
            //Add Task//
            AddNewProject();

            var p = projectController.Get(GetProjID(0));

            //Edit Task//
            p.ProjectName = "P23";
            p.EndDate = "10-10-2018";
            p.StartDate = "05-05-2018";
            p.Priority = 20;

            projectController.Put(p);

            //Check if Updated//
            ProjectViewModel t = projectController.Get(GetProjID(0));
            Assert.IsTrue(t.ProjectName == "P23");
        }
    }
}
