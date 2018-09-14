using ProjectManagerDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBL
{
    public class TaskDAO
    {
        public List<TaskViewModel> GetAll()
        {
            string parentTask = "";
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            //taskDBEntities.TaskDetails.Include(x => x.TaskDetail1);
            List<TaskDetail> task = taskDBEntities.TaskDetails.ToList();
            List<TaskViewModel> taskViewModelList = new List<TaskViewModel>();
            foreach (var t in task)
            {
                parentTask = "";
                string projName = "";
                if (t.ParentTaskID != null)
                    parentTask = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == t.ParentTaskID).TaskName;
                if (t.ProjectID != null)
                    projName = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectID == t.ProjectID).ProjectName;

                taskViewModelList.Add(
                    new TaskViewModel
                    (t.TaskID, t.TaskName, parentTask, projName, t.StartDate.ToShortDateString(), t.EndDate.ToShortDateString(), t.Priority,t.Status));
            }
            return taskViewModelList;
        }
        public TaskViewModel GetTask(int id)
        {
            string parentTask = "";
            string projName = "";
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            var task = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == id);
            if (task.ParentTaskID != null)
                parentTask = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == task.ParentTaskID).TaskName;

            if (task.ProjectID != null)
                projName = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectID == task.ProjectID).ProjectName;

            return new TaskViewModel
                (task.TaskID, task.TaskName, parentTask, projName, task.StartDate.ToShortDateString(), task.EndDate.ToShortDateString(), task.Priority,task.Status);

        }
        public void DeleteTask(int id)
        {
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            TaskDetail task = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == id);
            var entry = taskDBEntities.Entry(task);
            if (entry.State == System.Data.Entity.EntityState.Detached)
                taskDBEntities.TaskDetails.Attach(task);
            taskDBEntities.TaskDetails.Remove(task);
            taskDBEntities.SaveChanges();
        }
        public void AddTask(TaskViewModel taskVM)
        {
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            TaskDetail task = new TaskDetail();
            task.TaskName = taskVM.TaskName;
            if (taskVM.ParentTaskName != null && taskVM.ParentTaskName != "")
                task.ParentTaskID = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskName == taskVM.ParentTaskName).TaskID;

            if (taskVM.ProjectName != null && taskVM.ProjectName != "")
                task.ProjectID = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectName == taskVM.ProjectName).ProjectID;
            task.StartDate = Convert.ToDateTime(taskVM.StartDate);
            task.EndDate = Convert.ToDateTime(taskVM.EndDate);
            task.Priority = taskVM.Priority;
            task.Status = taskVM.Status;
            taskDBEntities.TaskDetails.Add(task);
            taskDBEntities.SaveChanges();

        }
        public void EditTask(TaskViewModel taskVM)
        {
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            TaskDetail task = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == taskVM.TaskID);
            if (taskVM.ParentTaskName != null && taskVM.ParentTaskName != "")
                task.ParentTaskID = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskName == taskVM.ParentTaskName).TaskID;

            if (taskVM.ProjectName != null && taskVM.ProjectName != "")
                task.ProjectID = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectName == taskVM.ProjectName).ProjectID;

            task.StartDate = Convert.ToDateTime(taskVM.StartDate);
            task.EndDate = Convert.ToDateTime(taskVM.EndDate);
            task.Priority = taskVM.Priority;

            taskDBEntities.TaskDetails.Attach(task);
            taskDBEntities.Entry(task).State = System.Data.Entity.EntityState.Modified;
            taskDBEntities.SaveChanges();

        }
    }
}
