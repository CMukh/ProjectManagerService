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
    class TaskServiceUnitTests
    {
        PrTaskController taskController;
        TaskDAO taskDao;

        [SetUp]
        public void Init()
        {
            taskDao = new TaskDAO();
            taskController = new PrTaskController(taskDao);
            DeleteAllTasks();

        }
        private void DeleteAllTasks()
        {
            //Delete All tasks
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();

            taskDBEntities.Database.ExecuteSqlCommand("delete from users");
            taskDBEntities.Database.ExecuteSqlCommand("delete from taskDetails");
            taskDBEntities.Database.ExecuteSqlCommand("delete from project");

        }
        private void AddNewTask()
        {
            TaskViewModel task = new TaskViewModel();
            //Add Task1//
            task.TaskName = "Task1";
            task.EndDate = "05-05-2018";
            task.StartDate = "01-01-2018";
            task.Priority = 10;
            taskController.Post(task);

            //Add project//
            ProjectController projectsController = new ProjectController(new ProjectDAO());
            ProjectViewModel p = new ProjectViewModel(1, "P1", "02-02-2018", "03-03-2018", 10, "");
            projectsController.Post(p);

            //Add Task2//
            TaskViewModel task1 = new TaskViewModel();
            task1.TaskName = "Task2";
            task1.ParentTaskName = "Task1";
            task1.ProjectName = "P1";
            task1.EndDate = "05-05-2018";
            task1.StartDate = "01-01-2018";
            task1.Priority = 10;
            taskController.Post(task1);
        }
        private int GetTaskID(int index)
        {
            //GetAllTasks
            IHttpActionResult response = taskController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<TaskViewModel>>;
            var t = contentResult.Content;

            //Count
            return t[index].TaskID;
        }
        private TaskViewModel GetTask(int index)
        {
            //GetAllTasks
            IHttpActionResult response = taskController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<TaskViewModel>>;
            var t = contentResult.Content;

            //Count
            return (TaskViewModel)t[index];
        }

        [Test]
        public void Get()
        {
            //Add Task//
            AddNewTask();

            //GetAllTasks
            IHttpActionResult response = taskController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<TaskViewModel>>;
            var t = contentResult.Content;

            //Count
            Assert.IsTrue(t.Where(tsk => tsk.TaskName == "Task1").Count() == 1);
        }

        [Test]
        public void GetTask()
        {
            //Add Task//
            AddNewTask();

            int id = GetTaskID(1);
            var t = taskController.Get(id);
            Assert.IsTrue(t.ParentTaskName == "Task1");
        }

        [Test]
        public void AddTask()
        {
            //Add Task//
            AddNewTask();

            //Check if Added//
            IHttpActionResult response = taskController.Get();
            var contentResult = response as OkNegotiatedContentResult<List<TaskViewModel>>;
            var t = contentResult.Content;
            Assert.IsTrue(t.Where(tsk => tsk.TaskName == "Task1").Count() == 1);
        }

        [Test]
        public void DeleteTask()
        {
            //Add 2 Tasks//
            AddNewTask();

            //Delete 1 task//
            taskController.Delete(GetTaskID(1));
            //Count
            IHttpActionResult r = taskController.Get();
            var c = r as OkNegotiatedContentResult<List<TaskViewModel>>;
            Assert.IsTrue(c.Content.Count() == 1);
        }

        [Test]
        public void EditTask()
        {
            //Add Task//
            AddNewTask();

            var task = GetTask(1);

            //Edit Task//
            task.TaskName = "Task1";
            task.EndDate = "10-10-2018";
            task.StartDate = "05-05-2018";
            task.Priority = 20;
            taskController.Put(task);

            //Check if Updated//
            var t = GetTask(1);
            Assert.IsTrue(t.Priority == 20);
        }
    }
}
