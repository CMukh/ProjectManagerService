using ProjectManagerDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBL
{
    public class UserDAO
    {
        public List<UserViewModel> GetAll()
        {

            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();

            List<User> user = taskDBEntities.Users.ToList();
            List<UserViewModel> userViewModelList = new List<UserViewModel>();
            foreach (var t in user)
            {
                string taskName = "";
                string projName = "";

                if (t.TaskID != null)
                    taskName = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == t.TaskID).TaskName;
                if (t.ProjectID != null)
                    projName = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectID == t.ProjectID).ProjectName;
                userViewModelList.Add(new UserViewModel
                    (t.UserID, t.FirstName, t.LastName, projName, t.EmployeeID, taskName));
            }
            return userViewModelList;
        }
        public UserViewModel GetUser(int id)
        {
            string taskName = "";
            string projName = "";
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            var user = taskDBEntities.Users.SingleOrDefault(p => p.UserID == id);
            if (user.TaskID != null)
                taskName = taskDBEntities.TaskDetails.SingleOrDefault(p => p.TaskID == user.TaskID).TaskName;
            if (user.ProjectID != null)
                projName = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectID == user.ProjectID).ProjectName;
            return new UserViewModel
                (user.UserID, user.FirstName, user.LastName, projName, user.EmployeeID, taskName);

        }
        public void DeleteUser(int id)
        {
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            User user = taskDBEntities.Users.SingleOrDefault(p => p.UserID == id);
            var entry = taskDBEntities.Entry(user);
            if (entry.State == System.Data.Entity.EntityState.Detached)
                taskDBEntities.Users.Attach(user);
            taskDBEntities.Users.Remove(user);
            taskDBEntities.SaveChanges();
        }
        public void AddUser(UserViewModel userVM)
        {
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            User user = new User();
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.EmployeeID = userVM.EmployeeID;


            if (userVM.TaskName != null && userVM.TaskName != "")
            {
                user.TaskID = taskDBEntities.TaskDetails.SingleOrDefault(t => t.TaskName == userVM.TaskName).TaskID;
            }
            if (userVM.ProjectName != null && userVM.ProjectName != "")
                user.ProjectID = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectName == userVM.ProjectName).ProjectID;

            taskDBEntities.Users.Add(user);
            taskDBEntities.SaveChanges();

        }
        public void EditUser(UserViewModel userVM)
        {

            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            User user = taskDBEntities.Users.SingleOrDefault(p => p.UserID == userVM.UserID);
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.EmployeeID = userVM.EmployeeID;
            if (userVM.TaskName != null && userVM.TaskName != "")
            {
                user.TaskID = taskDBEntities.TaskDetails.SingleOrDefault(t => t.TaskName == userVM.TaskName).TaskID;
            }
            if (userVM.ProjectName != null && userVM.ProjectName != "")
                user.ProjectID = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectName == userVM.ProjectName).ProjectID;

            taskDBEntities.Users.Attach(user);
            taskDBEntities.Entry(user).State = System.Data.Entity.EntityState.Modified;
            taskDBEntities.SaveChanges();

        }
    }
}
