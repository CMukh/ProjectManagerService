using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using ProjectManagerBL;
using ProjectManagerDL;
using ProjectManagerServices.Controllers;

namespace PMUnitTests
{
    class UserServiceUnitTests
    {
        PrUserController userController;
        UserDAO userDao;

        [SetUp]
        public void Init()
        {
            userDao = new UserDAO();
            userController = new PrUserController(userDao);
            DeleteAllUsers();

        }
        private void DeleteAllUsers()
        {
            //Delete All tasks
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();

            taskDBEntities.Database.ExecuteSqlCommand("delete from users");
            taskDBEntities.Database.ExecuteSqlCommand("delete from taskDetails");
            taskDBEntities.Database.ExecuteSqlCommand("delete from project");

        }
        private UserViewModel GetUser(int index)
        {
            //GetAllTasks
            IHttpActionResult response = userController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<UserViewModel>>;
            var t = contentResult.Content;

            //Count
            return (UserViewModel)t[index];
        }
        private void AddNewUser()
        {
            PrTaskController taskController = new PrTaskController(new TaskDAO());
            TaskViewModel task = new TaskViewModel();
            //Add Task1//
            task.TaskName = "Task1";
            task.EndDate = "05-05-2018";
            task.StartDate = "01-01-2018";
            task.Priority = 10;
            taskController.Post(task);

            //Add project//
            ProjectController projectsController = new ProjectController(new ProjectDAO());
            ProjectViewModel p = new ProjectViewModel(1, "Project1", "02-02-2018", "03-03-2018", 10, "");
            projectsController.Post(p);

            //Add User//
            UserViewModel usr = new UserViewModel();
            usr.EmployeeID = 1;
            usr.FirstName = "User1";
            usr.LastName = "zzz";
            usr.ProjectName = "Project1";
            usr.TaskName = "Task1";
            userController.Post(usr);
        }
        private int GetUserID(int index)
        {
            //GetAllTasks
            IHttpActionResult response = userController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<UserViewModel>>;
            var t = contentResult.Content;

            //Count
            return t[index].UserID;
        }
        [Test]
        public void Get()
        {
            //Add Task//
            AddNewUser();

            //GetAllTasks
            IHttpActionResult response = userController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<UserViewModel>>;
            var t = contentResult.Content;

            //Count
            Assert.IsTrue(t.Where(u => u.FirstName == "User1").Count() == 1);
        }

        [Test]
        public void GetUser()
        {
            //Add Task//
            AddNewUser();

            var t = userController.Get(GetUserID(0));
            Assert.IsTrue(t.FirstName == "User1");
        }
        [Test]
        public void DeleteUser()
        {
            //Add 2 Tasks//
            AddNewUser();

            //Delete 1 task//
            userController.Delete(GetUserID(0));

            //Check if Deleted//
            var response = userController.Get();
            var contentResult = response as NotFoundResult;
            // var t = contentResult.Content;
            Assert.IsNotNull(contentResult);
        }

        [Test]
        public void AddUser()
        {
            //Add Task//
            AddNewUser();

            //Check if Added//
            IHttpActionResult response = userController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<UserViewModel>>;
            var t = contentResult.Content;
            Assert.IsTrue(t.Where(u => u.FirstName == "User1").Count() == 1);
        }



        [Test]
        public void EditUser()
        {
            //Add User//
            AddNewUser();

            UserViewModel usr = new UserViewModel();
            usr.UserID = GetUserID(0);
            usr.EmployeeID = 1;
            usr.FirstName = "User1";
            usr.LastName = "zzz";
            usr.ProjectName = "Project1";
            usr.TaskName = "Task1";


            //Edit Task//
            usr.LastName = "TestLastName";
            userController.Put(usr);

            //Check if Updated//
            //GetAllUsers
            IHttpActionResult response = userController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<UserViewModel>>;
            var t = contentResult.Content;

            //Count
            Assert.IsTrue(t.Where(tsk => tsk.LastName == "TestLastName").Count() == 1);
        }

    }
}
